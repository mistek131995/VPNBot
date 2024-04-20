using Application.ControllerService.Common;
using MD5Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Payment.PayOk.Notification
{
    internal class Service : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var sign = $"{request.amount}|{request.payment_id}|{request.shop}|{request.currency}|{request.desc}|f035f6dde555705f6773f8f1a2ea5af7".GetMD5();

            if (sign == request.sign)
            {
                return true;
            }

            return false;
        }
    }
}
