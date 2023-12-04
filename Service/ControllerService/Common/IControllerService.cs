namespace Application.ControllerService.Common
{
    internal interface IControllerService<TRequest, TResult>
    {
        public Task<TResult> HandlingAsync(TRequest request);
    }
}
