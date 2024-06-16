namespace Core.Model.User
{
    public class ChangePasswordRequest
    {
        public Guid Guid { get; private set; }
        public string Password { get;private set; }

        public ChangePasswordRequest(Guid guid, string password)
        {
            Guid = guid;
            Password = password;
        }
    }
}
