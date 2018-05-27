using System;
using System.Globalization;
using Receipt.Repository;
using Receipt.ServiceModel;
using ServiceStack;
using ServiceStack.Configuration;
using Sima.Common.Constant;
using Sima.Common.Service;
using Ticket.Repository;

namespace Receip.ServiceInterface
{
    [RequiredRole("Agent")]
    public class ReceiptService : AuthOnlyService
    {
        private IReceiptRepository ReceiptRepo { get; }
        private ITicketRepository TicketRepo { get; }
        private ITicketStatusRepository TicketStatusRepo { get; }
        
        public ReceiptService(IReceiptRepository receiptRepo, ITicketRepository ticketRepo,
            ITicketStatusRepository ticketStatusRepo)
        {
            ReceiptRepo = receiptRepo;
            TicketRepo = ticketRepo;
            TicketStatusRepo = ticketStatusRepo;
        }

        public object Post(SubmitReceipt request)
        {
            //Create new receipt
            var receipt = SubmitReceipt(request);

            return new SubmitReceiptResponse()
            {
                Status = (int) CommonStatus.Success,
                Data = receipt.ConvertTo<Receipt.ServiceModel.Types.Receipt>()
            };
        }

        public object Post(PayReceipt request)
        {
            //Create new receipt
            var receipt = PayReceipt(request);

            return new PayReceiptResponse()
            {
                Status = (int) CommonStatus.Success,
                Data = receipt.ConvertTo<Receipt.ServiceModel.Types.Receipt>()
            };
        }

        public object Post(CancelReceipt request)
        {
            //Create new receipt
            var receipt = CancelReceipt(request);

            return new CancelReceiptResponse()
            {
                Status = (int) CommonStatus.Success,
                Data = receipt.ConvertTo<Receipt.ServiceModel.Types.Receipt>()
            };
        }

        private IReceipt SubmitReceipt(SubmitReceipt request)
        {
            //Check ticket is available
            var ticket = TicketRepo.GetTicket(request.TicketId);
            if (ticket == null)
            {
//                throw new Exception(TicketMessage.TicketDoesNotFound);
            }

            if (ticket.Status.Equals((int) TicketConstant.Lock))
            {
//                throw new Exception(TicketMessage.TicketIsNotAvail);
            }

            //Check departure date
            if (!DateTime.TryParseExact(request.DepartureDate, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var departureDate))
            {
//                throw new Exception(TicketMessage.InvalidDepartureDate);
            }

            //Check ticket does not booking before
            var status =
                TicketStatusRepo.GetTicketStatus(ticket.UserAuthId, ticket.Code, departureDate, ticket.DepartureTime);
            if (status.Status != (int) TicketStatusConstant.Available)
            {
//                throw new Exception(TicketMessage.TicketIsNotAvail);
            }

            //Create new receipt
            var newReceipt = new Receipt.Repository.OrmLite.Receipt();
            newReceipt.PopulateWith(ticket);
            newReceipt.AgentId = Convert.ToInt32(base.UserSession.UserAuthId);
            newReceipt.DepartureDate = departureDate;
            newReceipt.Note = request.Note;
            newReceipt.Status = (int) ReceiptStatusConstant.Submited;

            //Generate new code
            var code = ReceiptRepo.GenerateNewCode(ConfigUtils.GetAppSetting("receipt.code.len", 6));
            newReceipt.Code = code;

            //Create new receipt
            var receipt = ReceiptRepo.CreateReceipt(newReceipt);

            //Update ticket status
            var newStatus = TicketStatusRepo.UpdateTicketStatus(ticket.UserAuthId, ticket.Code, departureDate,
                ticket.DepartureTime, (int) TicketStatusConstant.Booked);

            return receipt;
        }

        private IReceipt PayReceipt(PayReceipt request)
        {
            //Check receipt is available
            var existingReceipt = ReceiptRepo.GetReceipt(request.Code);
            if (existingReceipt == null)
            {
//                throw new Exception(ReceiptMessage.ReceiptDoesNotExist);
            }

            //Check ticket is available
            var ticket = TicketRepo.GetTicket(existingReceipt.TicketId);
            if (ticket == null)
            {
//                throw new Exception(TicketMessage.TicketDoesNotFound);
            }

            //Check ticket does not sold before
            var status = TicketStatusRepo.GetTicketStatus(ticket.UserAuthId, ticket.Code, existingReceipt.DepartureDate,
                ticket.DepartureTime);
            if (status.Status.Equals((int) TicketStatusConstant.Sold))
            {
//                throw new Exception(TicketMessage.TickerIsSold);
            }

            //Update receipt
            var newReceipt = ReceiptRepo.UpdateReceipt(existingReceipt, ToPayReceipt(existingReceipt, request));

            //Update ticket status
            var newStatus = TicketStatusRepo.UpdateTicketStatus(ticket.UserAuthId, ticket.Code,
                newReceipt.DepartureDate, ticket.DepartureTime, (int) TicketStatusConstant.Sold);

            return newReceipt;
        }

        private static IReceipt ToPayReceipt(IReceipt existingReceipt, PayReceipt request)
        {
            var receipt = ((Receipt.Repository.OrmLite.Receipt) existingReceipt).CreateCopy();
            receipt.Status = (int) ReceiptStatusConstant.Paid;
            receipt.Note = request.Note;
            return receipt;
        }

        private IReceipt CancelReceipt(CancelReceipt request)
        {
            //Check receipt is available
            var existingReceipt = ReceiptRepo.GetReceipt(request.Code);
            if (existingReceipt == null)
            {
//                throw new Exception(ReceiptMessage.ReceiptDoesNotExist);
            }

            //Check ticket is available
            var ticket = TicketRepo.GetTicket(existingReceipt.TicketId);
            if (ticket == null)
            {
//                throw new Exception(TicketMessage.TicketDoesNotFound);
            }

            //Check ticket does not sold before
            var status = TicketStatusRepo.GetTicketStatus(ticket.UserAuthId, ticket.Code, existingReceipt.DepartureDate,
                ticket.DepartureTime);
            if (status.Status.Equals((int) TicketStatusConstant.Sold))
            {
//                throw new Exception(TicketMessage.TickerIsSold);
            }

            //Update receipt
            var newReceipt = ReceiptRepo.UpdateReceipt(existingReceipt, ToCancelReceipt(existingReceipt, request));

            //Update ticket status
            var newStatus = TicketStatusRepo.UpdateTicketStatus(ticket.UserAuthId, ticket.Code,
                newReceipt.DepartureDate, ticket.DepartureTime, (int) TicketStatusConstant.Available);

            return newReceipt;
        }

        private static IReceipt ToCancelReceipt(IReceipt existingReceipt, CancelReceipt request)
        {
            var receipt = ((Receipt.Repository.OrmLite.Receipt) existingReceipt).CreateCopy();
            receipt.Status = (int) ReceiptStatusConstant.Cancelled;
            receipt.Note = request.Note;
            return receipt;
        }
    }
}