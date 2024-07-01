using Core.Model.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryUnitTests.Common.Entities
{
    public static class VpnLocationTemplate
    {
        public static Location LocationWithServer = new Location("RU", "Russian", [new VpnServer("127.0.0.1", "Server 1", "Test", 1000, "mistek", "24041986")]);
    }
}
