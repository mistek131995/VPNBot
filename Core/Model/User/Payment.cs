using Core.Common;

namespace Core.Model.User
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccessPositionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid Guid { get; set; }
        public PaymentState State { get; set; }
    }
}
