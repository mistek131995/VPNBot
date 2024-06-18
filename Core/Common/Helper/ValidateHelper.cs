using System.Text.RegularExpressions;

namespace Core.Common.Helper
{
    public static class ValidateHelper
    {
        public static bool EmailVlidator(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            return match.Success;
        }
    }
}
