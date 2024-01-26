using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Register
{
    public class Request: IRequest<bool>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Token { get; set; }
    }
}
