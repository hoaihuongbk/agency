using Agency.RepositoryInterface;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Linq;

namespace Agency.Repository
{
    public class OrmLiteOperatorAgentRepository : OrmLiteOperatorAgentRepository<OperatorAgent>
    {
        public OrmLiteOperatorAgentRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public OrmLiteOperatorAgentRepository(IDbConnectionFactory dbFactory, string namedConnnection = null)
            : base(dbFactory, namedConnnection)
        {
        }
    }

    public class OrmLiteOperatorAgentRepository<TOperatorAgent> : IOperatorAgentRepository, IRequiresSchema, IClearable
        where TOperatorAgent : class, IOperatorAgent
    {
        private readonly IDbConnectionFactory _dbFactory;
        public string NamedConnection { get; private set; }

        protected OrmLiteOperatorAgentRepository(IDbConnectionFactory dbFactory, string namedConnnection = null)
        {
            this._dbFactory = dbFactory;
            this.NamedConnection = namedConnnection;
        }

        protected IDbConnection OpenDbConnection()
        {
            return this.NamedConnection != null
                ? _dbFactory.OpenDbConnection(NamedConnection)
                : _dbFactory.OpenDbConnection();
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

        public virtual IOperatorAgent CreateOperatorAgent(IOperatorAgent newOperatorAgent)
        {
            return Exec(db =>
            {
                AssertNoExistingOperatorAgent(db, newOperatorAgent);

                newOperatorAgent.Status = 1;
                newOperatorAgent.IsPrgCreatedDate = DateTime.Now;
                newOperatorAgent.IsPrgUpdatedDate = newOperatorAgent.IsPrgCreatedDate;

                db.Save((TOperatorAgent) newOperatorAgent);

                newOperatorAgent = db.SingleById<TOperatorAgent>(newOperatorAgent.Id);
                return newOperatorAgent;
            });
        }

        protected void AssertNoExistingOperatorAgent(IDbConnection db, IOperatorAgent newOperatorAgent,
            IOperatorAgent exceptForExistingOperatorAgent = null)
        {
            var existingOperatorAgent = GetOperatorAgent(newOperatorAgent.Id);
            if (existingOperatorAgent != null
                && (exceptForExistingOperatorAgent == null ||
                    existingOperatorAgent.Id != exceptForExistingOperatorAgent.Id))
                throw new ArgumentException("OperatorAgent is already exists");
        }

        public virtual void DeleteOperatorAgent(int id)
        {
            Exec(db =>
            {
                using (var trans = db.OpenTransaction())
                {
                    var item = GetOperatorAgent(id);

                    item.Status = 0;
                    item.IsPrgUpdatedDate = DateTime.Now;
                    db.Save((TOperatorAgent) item);

                    trans.Commit();
                }
            });
        }

        public virtual IOperatorAgent GetOperatorAgent(int id)
        {
            return Exec(db => { return db.Select<TOperatorAgent>(c => c.Id == id).FirstOrDefault(); });
        }

        public virtual void InitSchema()
        {
            Exec(db => { db.CreateTableIfNotExists<TOperatorAgent>(); });
        }

        public virtual void DropAndReCreateTables()
        {
            Exec(db => { db.DropAndCreateTable<TOperatorAgent>(); });
        }

        public virtual IOperatorAgent UpdateOperatorAgent(IOperatorAgent existingOperatorAgent,
            IOperatorAgent newOperatorAgent)
        {
            return Exec(db =>
            {
                AssertNoExistingOperatorAgent(db, newOperatorAgent, existingOperatorAgent);

                using (var trans = db.OpenTransaction())
                {
                    newOperatorAgent.Id = existingOperatorAgent.Id;
                    newOperatorAgent.IsPrgCreatedDate = existingOperatorAgent.IsPrgCreatedDate;
                    newOperatorAgent.IsPrgUpdatedDate = DateTime.Now;

                    db.Save((TOperatorAgent) newOperatorAgent);

                    trans.Commit();
                }


                return newOperatorAgent;
            });
        }

        public void Clear()
        {
            Exec(db => { db.DeleteAll<TOperatorAgent>(); });
        }

        public IOperatorAgent GetOperatorAgent(int operatorId, int agentId)
        {
            return Exec(db =>
            {
                return db.Select<TOperatorAgent>(c => c.OperatorId == operatorId && c.AgentId == agentId)
                    .FirstOrDefault();
            });
        }
    }
}