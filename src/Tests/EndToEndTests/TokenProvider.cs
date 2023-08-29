using Newtonsoft.Json;

namespace EndToEndTests
{

    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }

    public class TokenProvider
    {
        public async Task<string> GetAdminTokenAsync()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using var httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(ConfigProvider.GetIdentityApiUrl())
            };

            var response = await httpClient.PostAsync("connect/token", new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("client_id", "legacy-rpo"),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("scope", "apiscope"),
                new KeyValuePair<string, string>("password", "Pass123$"),
                new KeyValuePair<string, string>("username", "alice"),
            }));



            var rr = await response.Content.ReadAsStringAsync();

            var respData = JsonConvert.DeserializeObject<TokenResponse>(await response.Content.ReadAsStringAsync());
            return respData.AccessToken;
        }

        public async Task<string> GetVendorTokenAsync()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using var httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(ConfigProvider.GetIdentityApiUrl())
            };

            var response = await httpClient.PostAsync("connect/token", new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("client_id", "legacy-rpo"),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("scope", "apiscope"),
                new KeyValuePair<string, string>("password", "Pass123$"),
                new KeyValuePair<string, string>("username", "bob"),
            }));

            var rr = await response.Content.ReadAsStringAsync();

            var respData = JsonConvert.DeserializeObject<TokenResponse>(await response.Content.ReadAsStringAsync());
            return respData.AccessToken;
        }

        public async Task<string> GetCustomerTokenAsync()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using var httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(ConfigProvider.GetIdentityApiUrl())
            };

            var response = await httpClient.PostAsync("connect/token", new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("client_id", "legacy-rpo"),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("scope", "apiscope"),
                new KeyValuePair<string, string>("password", "Pass123$"),
                new KeyValuePair<string, string>("username", "tom"),
            }));


            var rr = await response.Content.ReadAsStringAsync();
            var respData = JsonConvert.DeserializeObject<TokenResponse>(await response.Content.ReadAsStringAsync());
            return respData.AccessToken;
        }
    }
}
