using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;

namespace PT.Common.Repository
{
    public class OrmLiteBaseRepository
    {
        private readonly IDbConnectionFactory dbFactory;
        public string NamedConnection { get; private set; }

        public OrmLiteBaseRepository(IDbConnectionFactory dbFactory, string namedConnnection = null)
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

        public void Exec(Action<IDbConnection> fn)
        {
            using (var db = OpenDbConnection())
            {
                fn(db);
            }
        }

        public T Exec<T>(Func<IDbConnection, T> fn)
        {
            using (var db = OpenDbConnection())
            {
                return fn(db);
            }
        }

        public IAuthSession UserSession
        {
            get
            {
                return HostContext.AppHost.TryGetCurrentRequest().GetSession();
            }
        }
    }
}
