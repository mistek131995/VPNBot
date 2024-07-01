using Core.Model.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryUnitTests.Common.Entities
{
    public static class AccessPositionTemplate
    {
        public static AccessPosition AccessPosition1 = new AccessPosition("Position1", 1, "Description", 100, "");
        public static AccessPosition AccessPosition2 = new AccessPosition("Position2", 2, "Description", 200, "");
    }
}
