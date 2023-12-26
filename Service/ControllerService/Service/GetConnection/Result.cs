using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.GetConnection
{
    public class Result
    {
        public string Name { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Protocol { get; set; }
        public string Guid { get; set; }
        public string Network {  get; set; }
        public string Security {  get; set; }
        public string PublicKey { get; set; }
        public string Fingerprint { get; set; }
        public string ServerName { get; set; }
        public string ShortId { get; set; }
    }
}
