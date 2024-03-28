namespace Service.ControllerService.Service.Admin.Users.GetUser
{
    public class Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? AccessEndDate { get; set; }
    }
}
