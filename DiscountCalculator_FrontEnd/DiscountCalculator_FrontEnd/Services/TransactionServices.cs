using DiscountCalculator_FrontEnd.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;

namespace DiscountCalculator_FrontEnd.Services
{
    public class TransactionServices
    {
        private readonly HttpClient _httpClient;
        public TransactionServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<TransactionModel>> GetTransactions()
        {
            try
            {
                _httpClient.Timeout = TimeSpan.FromSeconds(30);
                IEnumerable<TransactionModel> bpkbs = await _httpClient.GetFromJsonAsync<IEnumerable<TransactionModel>>("https://localhost:7247/api/CalculateDiscount/GetTransactions");

                return bpkbs;
            }
            catch (Exception ex)
            {
                return new List<TransactionModel>();
            }
        }
        public async Task SubmitTransaction(TransactionModel data)
        {
            try
            {

                var queryParams = new Dictionary<string, string>
                {
                    { "CustomerType", data.CustomerType },
                    { "PointReward", data.PointReward.ToString() },
                    { "TotalBelanja", data.TotalBelanja }                   
                };
                string baseUrl = "https://localhost:7247/api/CalculateDiscount/SaveTransaction";
                string urlWithQuery = QueryHelpers.AddQueryString(baseUrl, queryParams);

                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(urlWithQuery, "");


            }
            catch (Exception ex)
            {

            }
        }
    }
}
