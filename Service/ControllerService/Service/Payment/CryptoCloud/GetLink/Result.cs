using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Payment.CryptoCloud.GetLink
{
    internal class Result
    {
        public string status { get; set; }
        public _Result result { get; set; }

        public class Currency
        {
            public int id { get; set; }
            public string code { get; set; }
            public string fullcode { get; set; }
            public Network network { get; set; }
            public string name { get; set; }
            public bool is_email_required { get; set; }
            public bool stablecoin { get; set; }
            public string icon_base { get; set; }
            public string icon_network { get; set; }
            public string icon_qr { get; set; }
            public int order { get; set; }
        }

        public class Network
        {
            public string code { get; set; }
            public int id { get; set; }
            public string icon { get; set; }
            public string fullname { get; set; }
        }

        public class Project
        {
            public int id { get; set; }
            public string name { get; set; }
            public string fail { get; set; }
            public string success { get; set; }
            public string logo { get; set; }
        }

        public class _Result
        {
            public string uuid { get; set; }
            public string created { get; set; }
            public string address { get; set; }
            public string expiry_date { get; set; }
            public string side_commission { get; set; }
            public string side_commission_cc { get; set; }
            public double amount { get; set; }
            public double amount_usd { get; set; }
            public double amount_in_fiat { get; set; }
            public double fee { get; set; }
            public double fee_usd { get; set; }
            public double service_fee { get; set; }
            public double service_fee_usd { get; set; }
            public string type_payments { get; set; }
            public string fiat_currency { get; set; }
            public string status { get; set; }
            public bool is_email_required { get; set; }
            public string link { get; set; }
            public object invoice_id { get; set; }
            public Currency currency { get; set; }
            public Project project { get; set; }
            public bool test_mode { get; set; }
        }
    }
}
