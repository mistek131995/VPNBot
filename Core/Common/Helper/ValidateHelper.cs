using Service.ControllerService.Common;
using System.Text.RegularExpressions;

namespace Core.Common.Helper
{
    public static class ValidateHelper
    {
        public static void EmailVlidator(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            if (!match.Success)
                throw new HandledException("Неверный формат электронной почты");
        }

        public static void PasswordValidator(string password)
        {
            if(string.IsNullOrEmpty(password)) throw new HandledException("Пароль не может быть пустым");
            if(password.Length < 6) throw new HandledException("Длина пароля не может быть меньше 6 символов");
            if(password.Length > 12) throw new HandledException("Длина пароля не может быть больше 12 символов");
        }
    }
}
