using Core.Model.User;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public long TelegramUserId { get; set; }
        public string Login {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserSost Sost { get; set; }
        public UserRole Role { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? AccessEndDate { get; set; }
        public Guid Guid { get; set; }
        public int ParentUserId { get; set; }
        [Precision(18, 3)]
        public decimal Balance { get; set; }

        public DateTime? LastConnection { get; set; }

        public SubscribeType SubscribeType { get; set; }
        public string SubscribeToken { get; set; }

        public List<Payment> Payments { get; set; }
        public List<UserConnection> UserConnections { get; set; }
        public ChangePasswordRequest ChangePasswordRequest { get; set; }
        public ChangeEmailRequest ChangeEmailRequest { get; set; }
        public UserSetting UserSetting { get; set; }
    }
}
