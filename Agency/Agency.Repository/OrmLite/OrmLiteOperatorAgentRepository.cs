using System;
using System.Data;
using System.Linq;
using PT.Common.Repository.OrmLite;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Agency.Repository.OrmLite
{
    public class OrmLiteOperatorAgentRepository : OrmLiteOperatorAgentRepository<OperatorAgent>
    {
        public OrmLiteOperatorAgentRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }

    public class OrmLiteOperatorAgentRepository<TOperatorAgent> : OrmLiteBaseRepository, IOperatorAgentRepository, IRequiresSchema, IClearable
        where TOperatorAgent : class, IOperatorAgent
    {
        protected OrmLiteOperatorAgentRepository(IDbConnectionFactory dbFactory, string namedConnnection = null)
            : base(dbFactory, namedConnnection)
        {
            
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

        private void AssertNoExistingOperatorAgent(IDbConnection db, IOperatorAgent newOperatorAgent,
            IOperatorAgent exceptForExistingOperatorAgent = null)
        {
            var existingOperatorAgent = GetOperatorAgent(db, newOperatorAgent.Id);
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
            return Exec(db => GetOperatorAgent(db, id));
        }

        private static IOperatorAgent GetOperatorAgent(IDbConnection db, int id)
        {
            return db.Select<TOperatorAgent>(c => c.Id == id).FirstOrDefault(); 
        }

        public virtual void InitSchema()
        {
            Exec(db => { db.CreateTableIfNotExists<TOperatorAgent>(); });
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