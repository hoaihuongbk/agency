using System;
using System.Data;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace PT.Common.Repository.OrmLite
{
    public class OrmLiteBaseRepository
    {
        private readonly IDbConnectionFactory _dbFactory;
        private string NamedConnection { get; }

        protected OrmLiteBaseRepository(IDbConnectionFactory dbFactory, string namedConnnection = null)
        {
            _dbFactory = dbFactory;
            NamedConnection = namedConnnection;
        }

        private IDbConnection OpenDbConnection()
        {
            return NamedConnection != null
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

        public IAuthSession UserSession
        {
            get
            {
                return HostContext.AppHost.TryGetCurrentRequest().GetSession();
            }
        }
    }
}
