using Core.Model.User;

namespace Service.ControllerService.Service.App.GetSubscribeModal
{
    public class Result
    {
        public SubscribeType SubscribeType { get; set; }
        public List<Subscribe> Subscribes { get; set; }

        public class Subscribe
        {
            public int Id { get; set; }
            public string GooglePlayIdentifier { get; set; }
        }
    }
}
