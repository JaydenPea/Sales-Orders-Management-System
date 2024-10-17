using SalesOrders.Shared.ExternalCalls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrders.Shared.ExternalCalls
{
    public interface IExternalCallService
    {
        Task<ExternalCallVM> CallDogApi();
    }
}
