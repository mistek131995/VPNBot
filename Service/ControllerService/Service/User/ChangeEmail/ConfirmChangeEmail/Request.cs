﻿using Application.ControllerService.Common;

namespace Service.ControllerService.Service.User.ChangeEmail.ConfirmChangeEmail
{
    public class Request : IRequest<bool>
    {
        public string Guid { get; set; }
    }
}
