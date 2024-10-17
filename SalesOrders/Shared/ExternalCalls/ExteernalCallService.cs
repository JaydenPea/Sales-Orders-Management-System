using Microsoft.Extensions.Configuration;
using SalesOrders.Shared.ExternalCalls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrders.Shared.ExternalCalls
{
    public class ExteernalCallService : IExternalCallService
    {

        #region Injections
        private readonly IConfiguration _configuration;
        #endregion

        #region 
        public ExteernalCallService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        public async Task<ExternalCallVM> CallDogApi()
        {
            var baseAddress = _configuration.GetSection("ExternalApiBaseUrl:url").Value;
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);

            var response = await httpClient.GetAsync(baseAddress);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    ExternalCallVM vM = new ExternalCallVM();
                    return vM;
                }

                return await response.Content.ReadFromJsonAsync<ExternalCallVM>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status code: {response.StatusCode} message: {message}");
            }
        }
    }
}
