using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Payment.RuKassa.CreateLink
{
    public class Result
    {
        public int id { get; set; }
        public string hash { get; set; }
        public string url { get; set; }
    }
}
