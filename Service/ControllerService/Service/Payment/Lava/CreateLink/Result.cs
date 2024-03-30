using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Payment.Lava.CreateLink
{
    public class Result
    {
        public _Data data { get; set; }
        public int status { get; set; }
        public bool status_check { get; set; }

        public class _Data
        {
            public string id { get; set; }
            public int amount { get; set; }
            public string expired { get; set; }
            public int status { get; set; }
            public string shop_id { get; set; }
            public string url { get; set; }
            public object comment { get; set; }
            public string merchantName { get; set; }
            public object exclude_service { get; set; }
            public object include_service { get; set; }
        }
    }
}
