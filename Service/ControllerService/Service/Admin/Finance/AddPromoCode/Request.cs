﻿using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Finance.AddPromoCode
{
    public class Request : IRequest<bool>
    {
        public string Code { get; set; }
        public int Discount { get; set; }
        public int UsageCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
