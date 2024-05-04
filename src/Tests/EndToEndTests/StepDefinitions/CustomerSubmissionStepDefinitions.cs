using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http.Json;

namespace EndToEndTests.StepDefinitions
{
    public class CustomerSubmissionDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid VendorSubmissionId { get; set; }
    }

    public class CreateCustomerSubmissionRequest
    {
        public Guid CustomerId { get; set; }

        public Guid VendorSubmissionId { get; set; }

        public string VendorName { get; set; }

        public string ServiceFullName { get; set; }

        public string? Description { get; set; }
    }

    [Binding]
    public class CustomerSubmissionStepDefinitions
    {
        private readonly ScenarioContext _context;
        private readonly TokenProvider _tokenProvider = new TokenProvider();

        public CustomerSubmissionStepDefinitions(ScenarioContext context)
        {
            _context = context;
        }

        [Given(@"I am a client")]
        public async Task GivenIAmAClient()
        {

        }

        [Then(@"service  was deleted2")]
        public async Task ThenServiceWasDeleted()
        {
            var id = _context.Get<Guid>("service_id");
            var programId = _context.Get<Guid>("program_id");

            using var httpClient = GetHttpClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/administration/api/programs/{programId}/services/{id}")
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetAdminTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);
        }

        [Given(@"service  was created2")]
        public async Task GivenServiceWasCreated()
        {
            using var httpClient = GetHttpClient();

            var requestData = new CreateServiceRequest
            {
                Name = "Service" + Guid.NewGuid(),
                Description = "",
            };

            var programId = _context.Get<Guid>("program_id");

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/administration/api/programs/{programId}/services"),
                Content = JsonContent.Create(requestData)
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetAdminTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);

            _context.Set(requestData, "created_service_request_data");
            _context.Set(response, "created_service_response");
            var data = JsonConvert.DeserializeObject<ServiceDto>(await response.Content.ReadAsStringAsync());
            _context.Set(data, "created_service_response_data");
            _context.Set(data.Id, "service_id");
            _context.Set(data.Id, "created_service_id");
            _context.Set(data.Name, "service_name");
            _context.Set(data.Name, "created_service_name");
        }

        [Then(@"vendorSubmission was canceled2")]
        public async Task ThenvendorSubmissionWasCanceled()
        {
            var id = _context.Get<Guid>("vendorSubmission_id");

            using var httpClient = GetHttpClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/vendor/api/vendorSubmissions/{id}/cancel")
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetVendorTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);
        }

        [Given(@"vendorSubmission was created2")]
        public async Task GivenvendorSubmissionWasCreated()
        {
            using var httpClient = GetHttpClient();

            var requestData = new CreatevendorSubmissionRequest
            {
                VendorId = Guid.NewGuid(),
                ServiceId = _context.Get<Guid>("service_id"),
                Description = "",
                ServiceFullName = _context.Get<string>("service_name"),
                VendorName = "Bob"
            };

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/vendor/api/vendorSubmissions"),
                Content = JsonContent.Create(requestData)
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetVendorTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);

            _context.Set(requestData, "created_vendorSubmission_request_data");
            _context.Set(response, "created_vendorSubmission_response");
            var data = JsonConvert.DeserializeObject<vendorSubmissionDto>(await response.Content.ReadAsStringAsync());
            _context.Set(data, "created_vendorSubmission_response_data");
            _context.Set(data.Id, "vendorSubmission_id");
        }

        [When(@"I make a Get by id request in order to get an existing vendorSubmission2")]
        public async Task WhenIMakeAGetByIdRequestInOrderToGetAnExistingvendorSubmission()
        {
            var createdvendorSubmission = _context.Get<vendorSubmissionDto>("created_vendorSubmission_response_data");
            using var httpClient = GetHttpClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/vendor/api/vendorSubmissions/{createdvendorSubmission.Id}")
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetVendorTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);
            _context.Set(response, "get_by_id_vendorSubmission_response");
        }


        [Then(@"the response status code of get is '([^']*)' and the get vendorSubmission by id response data are valid2")]
        public async Task ThenTheResponseStatusCodeOfGetIsAndTheResponseDataAreValid1(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("get_by_id_vendorSubmission_response");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.OK}, but got {response.StatusCode}.");
            }

            var responseData = JsonConvert.DeserializeObject<vendorSubmissionDto>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(responseData.Id);
            // Assert.IsNotNull(responseData.Name);
            _context.Set(responseData.Id, "vendorSubmission_id");
        }

        [Given(@"vendorSubmision was created2")]
        public async Task GivenVendorSubmissionWasCreated()
        {
            using var httpClient = GetHttpClient();

            var requestData = new CreatevendorSubmissionRequest
            {
                VendorId = Guid.NewGuid(),
                ServiceId = _context.Get<Guid>("created_service_id"),
                Description = "",
                ServiceFullName = _context.Get<string>("created_service_name"),
                VendorName = "Bob"
            };

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/vendor/api/vendorSubmissions"),
                Content = JsonContent.Create(requestData)
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetVendorTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);

            var eee = await response.Content.ReadAsStringAsync();

            _context.Set(requestData, "created_vendorSubmission_request_data");
            _context.Set(response, "created_vendorSubmission_response");
            var data = JsonConvert.DeserializeObject<vendorSubmissionDto>(await response.Content.ReadAsStringAsync());
            _context.Set(data, "created_vendorSubmission_response_data");
            _context.Set(data.Id, "vendorSubmission_id");
        }

        [Then(@"vendorSubmision was canceled2")]
        public async Task ThenVendorSubmisionWasDeleted()
        {
            var id = _context.Get<Guid>("vendorSubmission_id");

            var createdvendorSubmission = _context.Get<vendorSubmissionDto>("created_vendorSubmission_response_data");
            using var httpClient = GetHttpClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/vendor/api/vendorSubmissions/{id}/cancel")
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetVendorTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);
        }

        private HttpClient GetHttpClient()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            return new HttpClient(clientHandler);
        }

        [When(@"I make a POST request in order to create a customerSubmission")]
        public async Task WhenIMakeAPOSTRequestInOrderToCreateAcustomerSubmission()
        {
            using var httpClient = GetHttpClient();

            var requestData = new CreateCustomerSubmissionRequest
            {
                CustomerId = Guid.NewGuid(),
                VendorSubmissionId = _context.Get<Guid>("vendorSubmission_id"),
                VendorName = "Bob",
                Description = "Description",
                ServiceFullName = "service"//_context.Get<string>("vendorSubmission_name"),
            };

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/customer/api/customerSubmissions"),
                Content = JsonContent.Create(requestData)
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetCustomerTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);
            var data = JsonConvert.DeserializeObject<CustomerSubmissionDto>(await response.Content.ReadAsStringAsync());

            _context.Set(requestData, "created_customerSubmission_request_data");
            _context.Set(response, "created_customerSubmission_response");
            _context.Set(data, "created_customerSubmissionresponse_data");
            _context.Set(data.Id, "created_customerSubmission_id");
        }

        [Given(@"customerSubmission was created")]
        public async Task GivencustomerSubmissionWasCreated()
        {
            using var httpClient = GetHttpClient();

            var requestData = new CreateCustomerSubmissionRequest
            {
                CustomerId = Guid.NewGuid(),
                VendorSubmissionId = _context.Get<Guid>("vendorSubmission_id"),
                VendorName = "Bob",
                Description = "Description",
                ServiceFullName = "service"//_context.Get<string>("vendorSubmission_name"),
            };

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/customer/api/customerSubmissions"),
                Content = JsonContent.Create(requestData)
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetCustomerTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);
            var data = JsonConvert.DeserializeObject<CustomerSubmissionDto>(await response.Content.ReadAsStringAsync());
            _context.Set(requestData, "created_customerSubmission_request_data");
            _context.Set(response, "created_customerSubmission_response");
            _context.Set(data, "created_customerSubmission_response_data");
            _context.Set(data.Id, "customerSubmission_id");
            _context.Set(data.Id, "created_customerSubmission_id");
        }

        [Then(@"customerSubmission was canceled")]
        public async Task ThencustomerSubmissionWasCanceled()
        {
            var id = _context.Get<Guid>("created_customerSubmission_id");
            using var httpClient = GetHttpClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/customer/api/customerSubmissions/{id}/cancel"),
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetCustomerTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);
        }

        [Then(@"the response status code is '([^']*)' and the customerSubmission response data are valid")]
        public async Task ThenTheResponseStatusCodeIsAndTheResponseDataAreValid(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("created_customerSubmission_response");
            var requestData = _context.Get<CreateCustomerSubmissionRequest>("created_customerSubmission_request_data");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.Created}, but got {response.StatusCode}.");
            }

            var responseData = JsonConvert.DeserializeObject<CustomerSubmissionDto>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(responseData.Id);
            //Assert.AreEqual(requestData.Name, responseData.Name);

            _context.Set(responseData, "created_cutomerSubmission_response_data");
            _context.Set(responseData.Id, "created_customerSubmission_id");
        }

        [When(@"I make a Cancel request in order to cancel an existing customerSubmission")]
        public async Task WhenIMakeADeleteRequestInOrderToCncelAnExistingcustomerSubmission()
        {
            var createdcustomerSubmission = _context.Get<CustomerSubmissionDto>("created_customerSubmission_response_data");

            using var httpClient = GetHttpClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/customer/api/customerSubmissions/{createdcustomerSubmission.Id}/cancel"),
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetCustomerTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);
            _context.Set(response, "canceled_customerSubmission_response");
        }

        [Then(@"the response status code of cancel  is '([^']*)' and the customerSubmission response data are valid")]
        public void ThenTheResponseStatusCodeOfCancelIsAndTheResponseDataAreValid(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("canceled_customerSubmission_response");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.NoContent}, but got {response.StatusCode}.");
            }
        }

        [When(@"I make a Get by id request in order to get an existing customerSubmission")]
        public async Task WhenIMakeAGetByIdRequestInOrderToGetAnExistingcustomerSubmission()
        {           
            var createdcustomerSubmission = _context.Get<CustomerSubmissionDto>("created_customerSubmission_response_data");

            using var httpClient = GetHttpClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/customer/api/customerSubmissions/{createdcustomerSubmission.Id}"),
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetCustomerTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);
            _context.Set(response, "get_by_id_customerSubmission_response");
        }

        [Then(@"the response status code of get is '([^']*)' and the customerSubmission response data are valid2")]
        public async Task ThenTheResponseStatusCodeOfGetIsAndTheResponseDataAreValid(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("get_by_id_customerSubmission_response");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.OK}, but got {response.StatusCode}.");
            }

            var responseData = JsonConvert.DeserializeObject<CustomerSubmissionDto>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(responseData.Id);
            // Assert.IsNotNull(responseData.Name);
            _context.Set(responseData.Id, "customerSubmission_id");
        }

        [Then(@"the response status code of get is '([^']*)' and the get customerSubmission by id response data are valid2")]
        public async Task ThenTheResponseStatusCodeOfGetIsAndTheResponseDataAreValid2(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("get_by_id_customerSubmission_response");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.OK}, but got {response.StatusCode}.");
            }

            var responseData = JsonConvert.DeserializeObject<CustomerSubmissionDto>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(responseData.Id);
            // Assert.IsNotNull(responseData.Name);
            _context.Set(responseData.Id, "customerSubmission_id");
        }
    }

}
