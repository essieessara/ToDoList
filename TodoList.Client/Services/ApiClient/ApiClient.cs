using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;


namespace TodoList.Client.Services.ApiClient
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        public ApiClient(IHttpClientFactory httpClient)
           => _httpClient = httpClient.CreateClient("backEnd");



        public async Task<T> PostAsync<T>(string URL, object Data)
        {
            string response = string.Empty;
            try
            {

                string serializedUser = JsonConvert.SerializeObject(Data);
                StringContent content = new StringContent(serializedUser,
                    System.Text.Encoding.UTF8, "application/json");
                var result = await _httpClient.PostAsync(URL, content);
                response = result.Content.ReadAsStringAsync().Result;
                result.EnsureSuccessStatusCode();
                T outPut = JsonConvert.DeserializeObject<T>(response);


                var handler = new JwtSecurityTokenHandler();
                string[] Token = response.Split(":");
                string[] Tokensplited = Token.LastOrDefault().Split("}");
                string TokenAfterSplit = Tokensplited.FirstOrDefault();
                string removeTokenQuotation = TokenAfterSplit.Replace('"', ' ').Trim();
                var decodedValue = handler.ReadJwtToken(removeTokenQuotation);

                var sid = decodedValue.Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).FirstOrDefault();
                var username = decodedValue.Claims.Where(c => c.Type == "unique_name")
                   .Select(c => c.Value).FirstOrDefault();
                var fullname = decodedValue.Claims.Where(c => c.Type == "given_name")
                   .Select(c => c.Value).FirstOrDefault();
                return outPut;

            }
            catch (Exception e)
            {
                throw new Exception(response);
            }
        }

    }
}
