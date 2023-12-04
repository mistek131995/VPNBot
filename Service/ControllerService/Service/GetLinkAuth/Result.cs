namespace Service.ControllerService.Service.GetLinkAuth
{
    public class Result
    {
        public UserState State { get; set; }

        public enum UserState
        {
            NotFound = 0,
            NeedSetPassword = 1,
            Ready = 2,
        }
    }
}
