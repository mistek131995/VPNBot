using Core.Model.User;

namespace Service.ControllerService.Service.Admin.Users.GetUser
{
    public class Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public DateTime? AccessEndDate { get; set; }
        public decimal Balance { get; set; }
    }
}
