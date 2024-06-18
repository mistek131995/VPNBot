using Service.ControllerService.Common;
using System.Text.RegularExpressions;

namespace Core.Model.User
{
    public class ChangeEmailRequest
    {
        public Guid Guid { get; private set; }
        public string Email { get; private set; }

        public ChangeEmailRequest(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            if (!match.Success)
                throw new HandledException("Неверный формат электронной почты");

            Guid = Guid.NewGuid();
            Email = email;
        }

        public void ChangeEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            if (!match.Success)
                throw new HandledException("Неверный формат электронной почты");

            Guid = Guid.NewGuid();
            Email = email;
        }
    }
}
