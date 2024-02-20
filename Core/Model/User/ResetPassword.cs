namespace Core.Model.User
{
    public class ResetPassword
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid Guid { get; set; }
    }
}
