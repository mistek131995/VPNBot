using Core.Common;
using Core.Model.Finance;
using Service.ControllerService.Common;
using System.Text.RegularExpressions;

namespace Core.Model.User
{
    public class User : IAggregate
    {
        public int Id { get; private set; }

        public long TelegramUserId { get;  private set; }
        public string Login { get;  private set; }
        public string Email { get;  private set; }
        public string Password { get;  private set; }
        public UserRole Role { get;  private set; }
        public DateTime RegisterDate { get;  private set; }
        public DateTime? AccessEndDate { get;  private set; }
        public UserSost Sost { get;  private set; }
        public Guid Guid { get;  private set; }
        public int ParentUserId { get;  private set; }
        public decimal Balance { get;  private set; }
        public DateTime? LastConnection { get;  private set; }

        public SubscribeType SubscribeType { get;  private set; }
        public string SubscribeToken { get;  private set; }

        public List<Payment> Payments { get;  private set; }
        public List<UserConnection> UserConnections { get;  private set; }
        public ChangePasswordRequest ChangePasswordRequest { get;  private set; }
        public ChangeEmailRequest ChangeEmailRequest { get;  private set; }
        public UserSetting UserSetting { get; private set; }

        public User(int id, long telegramUserId, string login, string email, string password, UserRole role, DateTime registerDate, DateTime? accessEndDate, UserSost sost, Guid guid, 
            int parentUserId, decimal balance, DateTime? lastConnection, SubscribeType subscribeType, string subscribeToken, List<Payment> payments, List<UserConnection> userConnections, UserSetting userSetting,
            ChangePasswordRequest changePasswordRequest = null, ChangeEmailRequest changeEmailRequest = null)
        {
            if(id == 0)
                throw new Exception("Нельзя использовать этот конструктор для создания сущности");

            Id = id;
            TelegramUserId = telegramUserId;
            Login = login.Trim().ToLower();
            Email = email.Trim().ToLower();
            Password = password;
            Role = role;
            RegisterDate = registerDate;
            AccessEndDate = accessEndDate;
            Sost = sost;
            Guid = guid;
            ParentUserId = parentUserId;
            Balance = balance;
            LastConnection = lastConnection;
            SubscribeType = subscribeType;
            SubscribeToken = subscribeToken;
            Payments = payments;
            UserConnections = userConnections;
            UserSetting = userSetting;
            ChangePasswordRequest = changePasswordRequest;
            ChangeEmailRequest = changeEmailRequest;
        }

        public User(long telegramUserId, string login, string email, string password, UserRole role, DateTime registerDate, DateTime? accessEndDate, UserSost sost, Guid guid, 
            int parentUserId, decimal balance, DateTime? lastConnection, SubscribeType subscribeType, string subscribeToken, List<Payment> payments, List<UserConnection> userConnections, UserSetting userSetting,
            ChangePasswordRequest changePasswordRequest, ChangeEmailRequest changeEmailRequest)
        {
            UpdateEmail(email);

            TelegramUserId = telegramUserId;
            Login = login.Trim().ToLower();
            Password = password;
            Role = role;
            RegisterDate = registerDate;
            AccessEndDate = accessEndDate;
            Sost = sost;
            Guid = guid;
            ParentUserId = parentUserId;
            Balance = balance;
            LastConnection = lastConnection;
            SubscribeType = subscribeType;
            SubscribeToken = subscribeToken;
            Payments = payments;
            UserConnections = userConnections;
            UserSetting = userSetting;
            ChangePasswordRequest = changePasswordRequest;
            ChangeEmailRequest = changeEmailRequest;
        }

        public User(string login, string email, string password, UserSost sost, UserSetting userSetting, int parentUserId = 0)
        {
            UpdateEmail(email);

            Login = login.Trim().ToLower();
            Password = password;
            Sost = sost;
            ParentUserId= parentUserId;

            AccessEndDate = DateTime.Now.AddDays(-1);
            Guid = Guid.NewGuid();
            RegisterDate = DateTime.Now;
            Role = UserRole.User;
            SubscribeToken = string.Empty;
            SubscribeType = SubscribeType.None;

            UserSetting = userSetting;
        }

        public void UpdateEmail(string email)
        {
            email = email.Trim().ToLower();

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            if (!match.Success)
                throw new HandledException("Неверный формат электронной почты");

            Email = email;
        }

        public void UpdatePassword(string password)
        {
            if(string.IsNullOrEmpty(password))
                throw new HandledException("Нельзя установить пустой пароль.");

            Password = password;
        }

        public void UpdateLastConnectionDate(DateTime lastConnectionDate)
        {
            LastConnection = lastConnectionDate;
        }

        public void UpdateAccessEndDate(int monthCount)
        {
            if (AccessEndDate < DateTime.Now)
                AccessEndDate = DateTime.Now.AddMonths(monthCount);
            else
                AccessEndDate = AccessEndDate?.AddMonths(monthCount);
        }

        public void UpdateAccessEndDate(DateTime date)
        {
            AccessEndDate = date;
        }

        public void SubtractBalance(int amount)
        {
            if(amount > Balance)
                throw new HandledException("Недостаточно бонусов для оплаты.");

            Balance -= amount;
        }

        public void UpdateSost(UserSost sost)
        {
            Sost = sost;
        }

        public void UpdateChangeEmailRequest(ChangeEmailRequest request)
        {
            ChangeEmailRequest = request;
        }

        public void UpdateChangePasswordRequest(ChangePasswordRequest request)
        {
            ChangePasswordRequest = request;
        }

        public void AttachTelegram(long telegramId)
        {
            if (telegramId <= 0)
                throw new HandledException("ID пользователя телеграм должен быть больше 0");

            TelegramUserId = TelegramUserId;
        }
    }
}
