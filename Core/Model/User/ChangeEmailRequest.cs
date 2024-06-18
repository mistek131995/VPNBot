using Core.Common.Helper;

namespace Core.Model.User
{
    public class ChangeEmailRequest
    {
        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public string Email { get; private set; }

        public ChangeEmailRequest(int id, Guid guid, string email)
        {
            if (id == 0)
                throw new Exception("Нельзя использовать этот конструктор для создания сущности");

            Id = id;
            Guid = guid;
            Email = email;
        }

        public ChangeEmailRequest(string email)
        {
            ValidateHelper.EmailVlidator(email);

            Guid = Guid.NewGuid();
            Email = email;
        }

        public void ChangeEmail(string email)
        {
            ValidateHelper.EmailVlidator(email);

            Guid = Guid.NewGuid();
            Email = email;
        }
    }
}
