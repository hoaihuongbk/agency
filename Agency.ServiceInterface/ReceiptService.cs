using Agency.RepositoryInterface;
using Agency.ServiceModel;
using ServiceStack;
using ServiceStack.Configuration;
using Sima.Common.Constant;
using Sima.Common.Service;
using System;
using System.Globalization;

namespace Agency.ServiceInterface
{
    [RequiredRole("Agent")]
    public class ReceiptService : AuthOnlyService
    {
        public IReceiptRepository receiptRepo { get; set; }
        public ITicketRepository ticketRepo { get; set; }
        public ITicketStatusRepository ticketStatusRepo { get; set; }

        public object Post(SubmitReceipt request)
        {
            //Create new receipt
            var receipt = SubmitReceipt(request);

            return new SubmitReceiptResponse()
            {
                Status = (int)CommonStatus.Success,
                Data = receipt.ConvertTo<ServiceModel.Types.Receipt>()
            };
        }
        public object Post(PayReceipt request)
        {
            //Create new receipt
            var receipt = PayReceipt(request);

            return new PayReceiptResponse()
            {
                Status = (int)CommonStatus.Success,
                Data = receipt.ConvertTo<ServiceModel.Types.Receipt>()
            };
        }
        public object Post(CancelReceipt request)
        {
            //Create new receipt
            var receipt = CancelReceipt(request);

            return new CancelReceiptResponse()
            {
                Status = (int)CommonStatus.Success,
                Data = receipt.ConvertTo<ServiceModel.Types.Receipt>()
            };
        }

        private IReceipt SubmitReceipt(SubmitReceipt request)
        {
            //Check ticket is available
            var ticket = ticketRepo.GetTicket(request.TicketId);
            if (ticket == null)
            {
//                throw new Exception(TicketMessage.TicketDoesNotFound);
            }
            if (ticket.Status.Equals((int)TicketConstant.Lock))
            {
//                throw new Exception(TicketMessage.TicketIsNotAvail);
            }

            //Check departure date
            DateTime departureDate;
            if (!DateTime.TryParseExact(request.DepartureDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out departureDate))
            {
//                throw new Exception(TicketMessage.InvalidDepartureDate);
            }

            //Check ticket does not booking before
            var status = ticketStatusRepo.GetTicketStatus(ticket.UserAuthId, ticket.Code, departureDate, ticket.DepartureTime);
            if (status.Status != (int)TicketStatusConstant.Available)
            {
//                throw new Exception(TicketMessage.TicketIsNotAvail);
            }

            //Create new receipt
            var newReceipt = new Repository.Receipt();
            newReceipt.PopulateWith(ticket);
            newReceipt.AgentId = Convert.ToInt32(UserSession.UserAuthId);
            newReceipt.DepartureDate = departureDate;
            newReceipt.Note = request.Note;
            newReceipt.Status = (int)ReceiptStatusConstant.Submited;

            //Generate new code
            var code = receiptRepo.GenerateNewCode(ConfigUtils.GetAppSetting("receipt.code.len", 6));
            newReceipt.Code = code;

            //Create new receipt
            var receipt = receiptRepo.CreateReceipt(newReceipt);

            //Update ticket status
            var newStatus = ticketStatusRepo.UpdateTicketStatus(ticket.UserAuthId, ticket.Code, departureDate, ticket.DepartureTime, (int)TicketStatusConstant.Booked);

            return receipt;
        }
        private IReceipt PayReceipt(PayReceipt request)
        {
            //Check receipt is available
            var existingReceipt = receiptRepo.GetReceipt(request.Code);
            if (existingReceipt == null)
            {
//                throw new Exception(ReceiptMessage.ReceiptDoesNotExist);
            }

            //Check ticket is available
            var ticket = ticketRepo.GetTicket(existingReceipt.TicketId);
            if (ticket == null)
            {
//                throw new Exception(TicketMessage.TicketDoesNotFound);
            }

            //Check ticket does not sold before
            var status = ticketStatusRepo.GetTicketStatus(ticket.UserAuthId, ticket.Code, existingReceipt.DepartureDate, ticket.DepartureTime);
            if (status.Status.Equals((int)TicketStatusConstant.Sold))
            {
//                throw new Exception(TicketMessage.TickerIsSold);
            }

            //Update receipt
            var newReceipt = receiptRepo.UpdateReceipt(existingReceipt, ToPayReceipt(existingReceipt, request));

            //Update ticket status
            var newStatus = ticketStatusRepo.UpdateTicketStatus(ticket.UserAuthId, ticket.Code, newReceipt.DepartureDate, ticket.DepartureTime, (int)TicketStatusConstant.Sold);

            return newReceipt;
        }
        private IReceipt ToPayReceipt(IReceipt existingReceipt, PayReceipt request)
        {
            var receipt = ((Repository.Receipt)existingReceipt).CreateCopy();
            receipt.Status = (int)ReceiptStatusConstant.Paid;
            receipt.Note = request.Note;
            return receipt;
        }
        private IReceipt CancelReceipt(CancelReceipt request)
        {
            //Check receipt is available
            var existingReceipt = receiptRepo.GetReceipt(request.Code);
            if (existingReceipt == null)
            {
//                throw new Exception(ReceiptMessage.ReceiptDoesNotExist);
            }
            //Check ticket is available
            var ticket = ticketRepo.GetTicket(existingReceipt.TicketId);
            if (ticket == null)
            {
//                throw new Exception(TicketMessage.TicketDoesNotFound);
            }
            //Check ticket does not sold before
            var status = ticketStatusRepo.GetTicketStatus(ticket.UserAuthId, ticket.Code, existingReceipt.DepartureDate, ticket.DepartureTime);
            if (status.Status.Equals((int)TicketStatusConstant.Sold))
            {
//                throw new Exception(TicketMessage.TickerIsSold);
            }

            //Update receipt
            var newReceipt = receiptRepo.UpdateReceipt(existingReceipt, ToCancelReceipt(existingReceipt, request));

            //Update ticket status
            var newStatus = ticketStatusRepo.UpdateTicketStatus(ticket.UserAuthId, ticket.Code, newReceipt.DepartureDate, ticket.DepartureTime, (int)TicketStatusConstant.Available);

            return newReceipt;
        }
        private IReceipt ToCancelReceipt(IReceipt existingReceipt, CancelReceipt request)
        {
            var receipt = ((Repository.Receipt)existingReceipt).CreateCopy();
            receipt.Status = (int)ReceiptStatusConstant.Cancelled;
            receipt.Note = request.Note;
            return receipt;
        }
    }
}
