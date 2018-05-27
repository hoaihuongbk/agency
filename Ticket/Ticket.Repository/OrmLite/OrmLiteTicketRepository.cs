using System;
using System.Data;
using System.Linq;
using PT.Common.Repository.OrmLite;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Ticket.Repository.OrmLite
{
    public class OrmLiteTicketRepository : OrmLiteTicketRepository<Ticket>
    {
        public OrmLiteTicketRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }

    public class OrmLiteTicketRepository<TTicket> : OrmLiteBaseRepository, ITicketRepository, IRequiresSchema,
        IClearable
        where TTicket : class, ITicket
    {
        protected OrmLiteTicketRepository(IDbConnectionFactory dbFactory, string namedConnnection = null)
            : base(dbFactory, namedConnnection)
        {
        }

        public virtual ITicket CreateTicket(ITicket newTicket)
        {
            return Exec(db =>
            {
                AssertNoExistingTicket(db, newTicket);

                newTicket.Status = 1;
                newTicket.IsPrgCreatedDate = DateTime.Now;
                newTicket.IsPrgUpdatedDate = newTicket.IsPrgCreatedDate;

                db.Save((TTicket) newTicket);

                newTicket = db.SingleById<TTicket>(newTicket.Id);
                return newTicket;
            });
        }

        private static void AssertNoExistingTicket(IDbConnection db, ITicket newTicket, ITicket exceptForExistingTicket = null)
        {
            var existingTicket = GetTicket(db, newTicket.Id);
            if (existingTicket != null
                && (exceptForExistingTicket == null || existingTicket.Id != exceptForExistingTicket.Id))
                throw new ArgumentException("Ticket is already exists");
        }

        public virtual void DeleteTicket(int id)
        {
            Exec(db =>
            {
                using (var trans = db.OpenTransaction())
                {
                    db.DeleteById<TTicket>(id);
                    trans.Commit();
                }
            });
        }

        public virtual ITicket GetTicket(int id)
        {
            return Exec(db => GetTicket(db, id));
        }

        private static ITicket GetTicket(IDbConnection db, int id)
        {
            return db.Select<TTicket>(c => c.Id == id).FirstOrDefault();
        }

        public virtual void InitSchema()
        {
            Exec(db => { db.CreateTableIfNotExists<TTicket>(); });
        }

        public virtual ITicket UpdateTicket(ITicket existingTicket, ITicket newTicket)
        {
            return Exec(db =>
            {
                AssertNoExistingTicket(db, newTicket, existingTicket);

                using (var trans = db.OpenTransaction())
                {
                    newTicket.Id = existingTicket.Id;
                    newTicket.IsPrgCreatedDate = existingTicket.IsPrgCreatedDate;
                    newTicket.IsPrgUpdatedDate = DateTime.Now;

                    db.Save((TTicket) newTicket);

                    trans.Commit();
                }


                return newTicket;
            });
        }

        public void Clear()
        {
            Exec(db => { db.DeleteAll<TTicket>(); });
        }
    }
}