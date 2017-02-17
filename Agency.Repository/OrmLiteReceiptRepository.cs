using Agency.RepositoryInterface;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Sima.Common.Helper;
using System;
using System.Data;
using System.Linq;
namespace Agency.Repository
{
    public class OrmLiteReceiptRepository : OrmLiteReceiptRepository<Receipt>
    {
        public OrmLiteReceiptRepository(IDbConnectionFactory dbFactory) : base(dbFactory) { }
        public OrmLiteReceiptRepository(IDbConnectionFactory dbFactory, string namedConnnection = null)
            : base(dbFactory, namedConnnection) { }
    }

	public class OrmLiteReceiptRepository<TReceipt > : IReceiptRepository, IRequiresSchema, IClearable
        
		 where TReceipt : class, IReceipt   		
		    {
		private readonly IDbConnectionFactory dbFactory;
        public string NamedConnection { get; private set; }
		
		public OrmLiteReceiptRepository(IDbConnectionFactory dbFactory, string namedConnnection = null)
        {
            this.dbFactory = dbFactory;
            this.NamedConnection = namedConnnection;
        }

        protected IDbConnection OpenDbConnection()
        {
            return this.NamedConnection != null
                ? dbFactory.OpenDbConnection(NamedConnection)
                : dbFactory.OpenDbConnection();
        }

        protected void Exec(Action<IDbConnection> fn)
        {
            using (var db = OpenDbConnection())
            {
                fn(db);
            }
        }

        protected T Exec<T>(Func<IDbConnection, T> fn)
        {
            using (var db = OpenDbConnection())
            {
                return fn(db);
            }
        }

		public virtual IReceipt CreateReceipt(IReceipt newReceipt)
        {
            return Exec(db =>
            {
                AssertNoExistingReceipt(db, newReceipt);

				newReceipt.Status = 1;
                newReceipt.IsPrgCreatedDate = DateTime.Now;
                newReceipt.IsPrgUpdatedDate = newReceipt.IsPrgCreatedDate;

                db.Save((TReceipt)newReceipt);

                newReceipt = db.SingleById<TReceipt>(newReceipt.Id);
                return newReceipt;
            });
        }

		protected void AssertNoExistingReceipt(IDbConnection db, IReceipt newReceipt, IReceipt exceptForExistingReceipt = null)
        {
            var existingReceipt = GetReceipt(newReceipt.Id);
            if (existingReceipt != null
                && (exceptForExistingReceipt == null || existingReceipt.Id != exceptForExistingReceipt.Id))
                throw new ArgumentException("Receipt is already exists");
        }

		public virtual void DeleteReceipt(int id)
        {
            Exec(db =>
            {
                using (var trans = db.OpenTransaction())
                {
					var item = GetReceipt(id);
					
                    item.Status = 0;
                    item.IsPrgUpdatedDate = DateTime.Now;
                    db.Save((TReceipt)item);

                    trans.Commit();
                }
            });
        }

		public virtual IReceipt GetReceipt(int id)
        {
            return Exec(db =>
            {
                return db.Select<TReceipt>(c => c.Id == id).FirstOrDefault();
            });
        }

		public virtual void InitSchema()
        {
            Exec(db =>
            {
									 db.CreateTableIfNotExists<TReceipt>();
				            });
        }

		public virtual void DropAndReCreateTables()
        {
            Exec(db =>
            {
									 db.DropAndCreateTable<TReceipt>();
				            });
        }

		public virtual IReceipt UpdateReceipt(IReceipt existingReceipt, IReceipt newReceipt)
        {
            return Exec(db =>
            {
                AssertNoExistingReceipt(db, newReceipt, existingReceipt);

                using (var trans = db.OpenTransaction())
                {
					
                    newReceipt.Id = existingReceipt.Id;
                    newReceipt.IsPrgCreatedDate = existingReceipt.IsPrgCreatedDate;
                    newReceipt.IsPrgUpdatedDate = DateTime.Now;

                    db.Save((TReceipt)newReceipt);

                    trans.Commit();
                }



                return newReceipt;
            });
        }

        public void Clear()
        {
            Exec(db =>
            {
									 db.DeleteAll<TReceipt>();
				            });
        }

        public string GenerateNewCode(int len = 6)
        {
            return Exec(db =>
            {
                var code = string.Empty;
                do
                {
                    code = SvcHelper.GetUniqueKey(len);
                } while (db.Exists<TReceipt>(c => c.Code == code));
                return code;
            });
        }

        public IReceipt GetReceipt(string code)
        {
            return Exec(db =>
            {
                return db.Select<TReceipt>(c => c.Code == code.Trim()).FirstOrDefault();
            });
        }
    }
}
