using Agency.RepositoryInterface;
using Agency.ServiceModel;
using ServiceStack;
using Sima.Common.Constant;
using Sima.Common.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Agency.ServiceInterface
{
   
    public class TicketService : AuthOnlyService
    {
        private ITicketRepository TicketRepo { get; set; }
        private ITicketAgentRepository TicketAgentRepo { get; set; }
        private ITicketStatusRepository TicketStatusRepo { get; set; }

        [RequiredRole("Agent")]
        public object Any(GetTickets request)
        {
            if(!DateTime.TryParseExact(request.DepartureDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out var departureDate))
            {
//                throw new Exception(TicketMessage.InvalidDepartureDate);
            }
            var tickets = TicketAgentRepo.GetTickets(Convert.ToInt32(UserSession.UserAuthId), departureDate, request.NumRowPerPage, request.Page).ToList();
            if (tickets.IsNullOrEmpty())
            {
//                throw new Exception(TicketMessage.TicketDoesNotFound);
            }

            //Map with status
            var data = new List<ServiceModel.Types.TicketAgent>();
            tickets.ForEach(c =>
            {
                var ticket = c.ConvertTo<ServiceModel.Types.TicketAgent>();
                if(ticket.Status.Equals((int)TicketConstant.Available))
                {
                    var status = TicketStatusRepo.GetTicketStatus(c.OperatorId, c.Code, departureDate, c.DepartureTime);
                    switch (status.Status)
                    {
                        case (int)TicketStatusConstant.Available:
                            ticket.Status = (int)TicketConstant.Available;
                            break;
                        default:
                            ticket.Status = (int)TicketConstant.Lock;
                            break;
                    }
                }
              
                data.Add(ticket);
            });

            return new GetTicketsResponse()
            {
                Status = (int)CommonStatus.Success,
                Data = data
            };
        }

        [RequiredRole("Operator")]
        public object Post(AddTicket request)
        {
            var ticket = TicketRepo.CreateTicket(ToCreateTicket(request));

            return new AddTicketResponse()
            {
                Status = (int)CommonStatus.Success,
                Data = ticket.ConvertTo<ServiceModel.Types.Ticket>()
            };
        }

        [RequiredRole("Operator")]
        public object Post(UpdateTicket request)
        {
            var existingTicket = TicketRepo.GetTicket(request.Id);
            if(existingTicket == null)
            {
//                throw new Exception(TicketMessage.TicketDoesNotFound);
            }
            var ticket = TicketRepo.UpdateTicket(existingTicket, ToUpdateTicket(existingTicket, request));

            return new AddTicketResponse()
            {
                Status = (int)CommonStatus.Success,
                Data = ticket.ConvertTo<ServiceModel.Types.Ticket>()
            };
        }

        private ITicket ToCreateTicket(AddTicket request)
        {
            DateTime? fromDate = null, toDate = null;
            if (!string.IsNullOrEmpty(request.StartDate))
            {
                fromDate = DateTime.ParseExact(request.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault);
                //throw new Exception(TicketMessage.InvalidFromDate);
            }
            if (!string.IsNullOrEmpty(request.EndDate))
            {
                toDate = DateTime.ParseExact(request.EndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault);
                //throw new Exception(TicketMessage.InvalidToDate);
            }
            var ticket = request.ConvertTo<Repository.Ticket>();
            ticket.UserAuthId = Convert.ToInt32(UserSession.UserAuthId);
            ticket.FromDate = fromDate;
            ticket.ToDate = toDate;
            ticket.Status = (int)TicketStatusConstant.Available;

            return ticket;
        }
        private ITicket ToUpdateTicket(ITicket existingTicket, UpdateTicket request)
        {
            DateTime? fromDate = null, toDate = null;
            if (!string.IsNullOrEmpty(request.StartDate))
            {
                fromDate = DateTime.ParseExact(request.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault);
                //throw new Exception(TicketMessage.InvalidFromDate);
            }
            if (!string.IsNullOrEmpty(request.EndDate))
            {
                toDate = DateTime.ParseExact(request.EndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault);
                //throw new Exception(TicketMessage.InvalidToDate);
            }
            var ticket = ((Repository.Ticket)existingTicket).CreateCopy();
            ticket.PopulateWith(request);
            ticket.FromDate = fromDate;
            ticket.ToDate = toDate;

            return ticket;
        }
    }
}
