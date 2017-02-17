using Agency.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Repository
{
    public class TicketStatus : ITicketStatus
    {
        public int Status { get; set; }
    }
}
