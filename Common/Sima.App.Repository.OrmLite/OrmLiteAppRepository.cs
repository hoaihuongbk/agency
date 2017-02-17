using PT.Common.Repository;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Linq;
namespace Sima.App.Repository.OrmLite
{
    public class OrmLiteAppRepository : OrmLiteAppRepository<App>
    {
        public OrmLiteAppRepository(IDbConnectionFactory dbFactory) : base(dbFactory) { }
        public OrmLiteAppRepository(IDbConnectionFactory dbFactory, string namedConnnection = null)
            : base(dbFactory, namedConnnection) { }
    }

    public class OrmLiteAppRepository<TApp> : OrmLiteBaseRepository, IAppRepository, IRequiresSchema, IClearable

         where TApp : class, IApp
    {

        public OrmLiteAppRepository(IDbConnectionFactory dbFactory, string namedConnnection = null)
        : base(dbFactory, namedConnnection) { }

        public virtual IApp CreateApp(IApp newApp)
        {
            return Exec(db =>
            {
                AssertNoExistingApp(db, newApp);

                newApp.Status = 1;
                newApp.IsPrgCreatedDate = DateTime.Now;
                newApp.IsPrgUpdatedDate = newApp.IsPrgCreatedDate;

                db.Save((TApp)newApp);

                newApp = db.SingleById<TApp>(newApp.Id);
                return newApp;
            });
        }

        protected void AssertNoExistingApp(IDbConnection db, IApp newApp, IApp exceptForExistingApp = null)
        {
            var existingApp = GetApp(newApp.Name);
            if (existingApp != null
                && (exceptForExistingApp == null || existingApp.Id != exceptForExistingApp.Id))
                throw new ArgumentException("App is already exists");
        }

        public virtual void DeleteApp(int id)
        {
            Exec(db =>
            {
                using (var trans = db.OpenTransaction())
                {
                    db.DeleteById<TApp>(id);
                    trans.Commit();
                }
            });
        }

        public virtual IApp GetApp(int id)
        {
            return Exec(db =>
            {
                return db.Select<TApp>(c => c.Id == id).FirstOrDefault();
            });
        }

        public virtual IApp GetApp(string name)
        {
            return Exec(db =>
            {
                return db.Select<TApp>(c => c.Name.ToLower() == name.Trim().ToLower()).FirstOrDefault();
            });
        }

        public virtual void InitSchema()
        {
            Exec(db =>
            {
                db.CreateTableIfNotExists<TApp>();
            });
        }

        public virtual void DropAndReCreateTables()
        {
            Exec(db =>
            {
                db.DropAndCreateTable<TApp>();
            });
        }

        public virtual IApp UpdateApp(IApp existingApp, IApp newApp)
        {
            return Exec(db =>
            {
                AssertNoExistingApp(db, newApp, existingApp);

                using (var trans = db.OpenTransaction())
                {
                    newApp.Id = existingApp.Id;
                    newApp.IsPrgCreatedDate = existingApp.IsPrgCreatedDate;
                    newApp.IsPrgUpdatedDate = DateTime.Now;

                    db.Save((TApp)newApp);

                    trans.Commit();
                }



                return newApp;
            });
        }

        public void Clear()
        {
            Exec(db =>
            {
                db.DeleteAll<TApp>();
            });
        }

        public IApp GetAppByCode(string code)
        {
            return Exec(db =>
            {
                return db.Select<TApp>(c => c.Code.ToLower() == code.Trim().ToLower()).FirstOrDefault();
            });
        }

        //public IApp GetApp(IAuthSession session, IAuthTokens tokens = null)
        //{
        //    var userAuth = userAuthRepo.GetUserAuth(session, tokens);
        //    if(userAuth != null)
        //    {
        //        return GetApp(Convert.ToInt32(userAuth.RefId));
        //    }
        //    throw new ArgumentException("App does not found");
        //}
    }
}
