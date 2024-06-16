namespace Core.Model.User
{
    public class ChangeEmailRequest
    {
        public Guid Guid { get; private set; }
        public string Email { get; private set; }

        public ChangeEmailRequest(Guid guid, string email)
        {
            Guid = guid;
            Email = email;
        }
    }
}
