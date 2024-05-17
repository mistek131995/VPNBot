namespace Service.ControllerService.Service.Admin.Users.GetUsers
{
    public class Result
    {
        public List<User> Users { get; set; }
        public int Count { get; set; }

        public record User(int Id, string Name, DateTime RegisterDate, DateTime? AccessEndDate, DateTime? LastConnection);
    }
}
