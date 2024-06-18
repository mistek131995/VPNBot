using Core.Common.Helper;

namespace Core.Model.User
{
    public class ChangePasswordRequest
    {
        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public string Password { get;private set; }

        public ChangePasswordRequest(int id, Guid guid, string password)
        {
            if(id == 0)
                throw new Exception("Нельзя использовать этот конструктор для создания сущности");

            Id = id;
            Guid = guid;
            Password = password;
        }

        public ChangePasswordRequest(Guid guid, string password)
        {
            Guid = guid;
            Password = password;
        }

        public void ChangePassword(string password)
        {
            ValidateHelper.PasswordValidator(password);

            Password = password;
        }
    }
}
