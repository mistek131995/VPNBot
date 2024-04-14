using Core.Common;

namespace Core.Model.Finance
{
    public class AccessPosition : IAggregate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MonthCount { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
