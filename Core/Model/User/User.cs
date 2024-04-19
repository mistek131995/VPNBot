using Core.Common;
using System.Collections.ObjectModel;

namespace Core.Model.User
{
    public class User : IAggregate
    {
        public int Id { get; set; }

        public long TelegramUserId { get; set; }
        public long TelegramChatId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? AccessEndDate { get; set; }
        public UserSost Sost { get; set; }
        public Guid Guid { get; set; }
        public int ParentUserId {  get; set; }
        public decimal Balance { get; set; }

        public List<Payment> Payments { get; set; }
        public List<UserConnection> UserConnections { get; set; }
        public List<UserUsedPromoCode> UserUsedPromoCodes { get; set; }
    }
}
