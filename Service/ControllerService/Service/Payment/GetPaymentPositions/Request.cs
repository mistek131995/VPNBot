﻿using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.GetPaymentPositions
{
    public class Request : IRequest<Result>
    {
        public int UserId { get; set; }
    }
}
