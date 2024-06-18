using Core.Common;
using Service.ControllerService.Common;
using System.Text.RegularExpressions;

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
        public int ParentUserId { get; set; }
        public decimal Balance { get; set; }
        public DateTime? LastConnection { get; set; }

        public SubscribeType SubscribeType { get; set; }
        public string SubscribeToken { get; set; }

        public void ChangeEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            if (!match.Success)
                throw new HandledException("Неверный формат электронной почты");

            Email = email;
        }

        public List<Payment> Payments { get; set; }
        public List<UserConnection> UserConnections { get; set; }
        public ChangePasswordRequest ChangePasswordRequest { get; set; }
        public ChangeEmailRequest ChangeEmailRequest { get; set; }
    }
}
