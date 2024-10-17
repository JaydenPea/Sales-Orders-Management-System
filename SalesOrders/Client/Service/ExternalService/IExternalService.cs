using SalesOrders.Shared.ExternalCalls.Models;

namespace SalesOrders.Client.Service.ExternalService
{
    public interface IExternalService
    {
        Task<ExternalCallVM> externalCall();

    }
}
