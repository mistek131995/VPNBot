using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Admin.GetStatistics
{
    public class Result
    {
        public int RegisterToday { get; set; }
        public int RegisterTomorrow { get; set; }
        public int ConnectionCount { get; set; }

        public record ConnectionByLocation(string Name, int Count);
    }
}
