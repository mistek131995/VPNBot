using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.RuKassa.Notification
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var signature = Signature.GenerateSignature(request.Query, "f1bcf17bb8a0a91966e6bb55b20e6761");

            Console.WriteLine(signature);
            Console.WriteLine(request.Signature);

            return true;
        }
    }
}
