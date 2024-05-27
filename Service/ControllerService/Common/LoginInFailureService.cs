namespace Service.ControllerService.Common
{
    public class LoginInFailureService
    {
        private List<LoginFailure> LoginFailures { get; set; } = new List<LoginFailure>();
        private List<BlockIP> BlockIPs { get; set; } = new List<BlockIP>();

        public void CheckLoginInRequest(string ip)
        {
            if (BlockIPs.Any(x => x.Ip == ip && (DateTime.Now - x.Date).TotalMinutes < 15))
                throw new HandledException("Превышено кол-во попыток входа, вы заблокированы на 15мин.");
        }


        public void AddFailure(string ip, int userId)
        {
            var loginFailureItem = LoginFailures.FirstOrDefault(x => x.Ip == ip && x.UserId == userId);

            if (loginFailureItem != null)
            {
                loginFailureItem.Count += 1;
                loginFailureItem.Date = DateTime.Now;
            }
            else
            {
                LoginFailures.Add(new LoginFailure()
                {
                    Ip = ip,
                    UserId = userId,
                    Count = 1,
                    Date = DateTime.Now,
                    ShowUserNotification = true
                });
            }

            var loginFailureItems = LoginFailures
                .Where(x => x.Ip == ip && (DateTime.Now - x.Date).TotalMinutes < 15)
                .Sum(x => x.Count);

            if (loginFailureItems > 3)
            {
                var blockIP = BlockIPs.FirstOrDefault(x => x.Ip == ip);

                if(blockIP == null)
                {
                    BlockIPs.Add(new BlockIP()
                    {
                        Ip = ip,
                        Date = DateTime.Now,
                    });
                }
                else
                {
                    blockIP.Date = DateTime.Now;
                }

                LoginFailures.RemoveAll(x => x.Ip == ip);
            }
                
        }

        public class LoginFailure()
        {
            public string Ip { get; set; }
            public int Count { get; set; }
            public DateTime Date { get; set; }
            public int UserId { get; set; }
            public bool ShowUserNotification { get; set; }
        }

        public class BlockIP()
        {
            public string Ip { get; set; }
            public DateTime Date { get; set; }
        }
    }
}
