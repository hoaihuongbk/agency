﻿using ServiceStack;
using Sima.Common.Model;

namespace Receipt.ServiceModel
{
    [Route("/receipt/cancel")]
    public class CancelReceipt : BaseRequest, IReturn<CancelReceiptResponse>
    {
        public string Code { get; set; }
        public string Note { get; set; }
    }
}