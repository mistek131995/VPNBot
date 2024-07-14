using Core.Common;

namespace Core.Model.User
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public int AccessPositionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public PaymentState State { get; set; }
        public int PromoCodeId { get; set; }
        public Guid Guid { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public Payment(int id, int accessPositionId, decimal amount, DateTime date, PaymentState state, int promoCodeId, Guid guid, PaymentMethod paymentMethod)
        {
            if (id == 0)
                throw new Exception("Нельзя использовать этот конструктор для создания сущности");

            Id = id;
            AccessPositionId = accessPositionId;
            Amount = amount;
            Date = date;
            State = state;
            PromoCodeId = promoCodeId;
            Guid = guid;
            PaymentMethod = paymentMethod;
        }

        public Payment(int accessPositionId, decimal amount, DateTime date, PaymentState state, int promoCodeId, Guid guid, PaymentMethod paymentMethod)
        {
            AccessPositionId = accessPositionId;
            Amount = amount;
            Date = date;
            State = state;
            PromoCodeId = promoCodeId;
            Guid = guid;
            PaymentMethod = paymentMethod;
        }
    }
}
