namespace Core.Model.User
{
    public class Activation
    {
        public int Id { get; }
        public int UserId { get; set; }
        public Guid Guid { get; set; }

        public Activation(int id, int userId, Guid guid)
        {
            Id = id;
            UserId = userId;
            Guid = guid;
        }
    }
}
