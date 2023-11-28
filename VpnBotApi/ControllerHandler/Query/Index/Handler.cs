using Database.Common;
using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.Query.Index
{
    public class Handler(IRepositoryProvider repositoryProvider) : IControllerHandler<Query, Response>
    {
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            var user = await repositoryProvider.UserRepository.GetByIdAsync(query.UserId);

            response.Login = user.Login;
            response.RegisterDate = user.RegisterDate;
            response.EndAccessDate = user.Access.EndDate;

            if(user.Payments != null && user.Payments.Any())
            {
                response.Payments = user.Payments.Select(x => new Response.Payment()
                {
                    Id = x.Id,
                    Date = x.Date,
                    Range = $"{x.AccessPosition.MonthCount} месяц.",
                    Price = $"{x.AccessPosition.Price} руб."
                }).ToList();
            }

            return response;
        }
    }
}
