using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Admin.Finance.GetPromoCodes
{
    public class Result
    {
        public List<PromoCode> PromoCodes { get; set; }

        public class PromoCode()
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public int Discount { get; set; }
            public int UsageCount { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
    }
}
