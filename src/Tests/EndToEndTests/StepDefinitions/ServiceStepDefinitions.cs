using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http.Json;

namespace EndToEndTests.StepDefinitions
{
    public class ServiceDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public Guid ProgramId { get; set; }

        public DateTime Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }

    public class UpdateServiceRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime LastModified { get; set; }
    }

    [Binding]
    public class ServiceStepDefinitions
    {
        private readonly ScenarioContext _context;
        private readonly TokenProvider _tokenProvider = new TokenProvider();

        public ServiceStepDefinitions(ScenarioContext context)
        {
            _context = context;
        }

        [Given(@"I am a client")]
        public void GivenIAmAClient()
        {
        }

        [Given(@"program  was created")]
        public async Task GivenProductWasCreated()
        {
            using var httpClient = GetHttpClient();

            var requestData = new CreateProgramRequest
            {
                Name = "Program" + Guid.NewGuid(),
                Description = "",

                StateId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(365)
            };

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/administration/api/Programs"),
                Content = JsonContent.Create(requestData)
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetAdminTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);
            var message = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<ProgramDto>(await response.Content.ReadAsStringAsync());
            _context.Set(requestData, "created_program_request_data");
            _context.Set(response, "created_program_response");
            _context.Set(data, "created_program_response_data");
            _context.Set(data.Id, "created_program_id");
            _context.Set(data.Id, "program_id");
        }

        [When(@"I make a POST request in order to create a service")]
        public async Task WhenIMakeAPOSTRequestInOrderToCreateAService()
        {
            using var httpClient = GetHttpClient();

            var requestData = new CreateServiceRequest
            {
                Name = "Service" + Guid.NewGuid(),
                Description = "",
                ProgramId = _context.Get<Guid>("created_program_id")
            };

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/administration/api/Services"),
                Content = JsonContent.Create(requestData)
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetAdminTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);
            var data = JsonConvert.DeserializeObject<ServiceDto>(await response.Content.ReadAsStringAsync());

            _context.Set(requestData, "created_service_request_data");
            _context.Set(response, "created_service_response");
            _context.Set(data, "created_serviceresponse_data");
            _context.Set(data.Id, "created_service_id");
        }

        [Then(@"service was deleted")]
        public async Task ThenServiceWasDeleted()
        {
            var id = _context.Get<Guid>("service_id");

            using var httpClient = GetHttpClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/administration/api/Services/{id}")
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetAdminTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);


        }

        [Then(@"program was deleted")]
        public async Task ThenProgramWasDeleted()
        {
            var id = _context.Get<Guid>("program_id");

            using var httpClient = GetHttpClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/administration/api/Programs/{id}")
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetAdminTokenAsync());
            var response = await httpClient.SendAsync(httpRequestMessage);

        }

        [Then(@"the response status code is '([^']*)' and the service response data are valid")]
        public async Task ThenTheResponseStatusCodeIsAndTheResponseDataAreValid(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("created_service_response");
            var requestData = _context.Get<CreateServiceRequest>("created_service_request_data");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.Created}, but got {response.StatusCode}.");
            }

            var responseData = JsonConvert.DeserializeObject<ServiceDto>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(responseData.Id);
            Assert.AreEqual(requestData.Name, responseData.Name);

            _context.Set(responseData, "created_seervice_response_data");
            _context.Set(responseData.Id, "service_id");
        }

        [Given(@"service was created")]
        public async Task GivenServiceWasCreated()
        {
            using var httpClient = GetHttpClient();

            var requestData = new CreateServiceRequest
            {
                Name = "Service" + Guid.NewGuid(),
                Description = "",
                ProgramId = _context.Get<Guid>("program_id")
            };

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/administration/api/Services"),
                Content = JsonContent.Create(requestData)
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetAdminTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);

            _context.Set(requestData, "created_service_request_data");
            _context.Set(response, "created_service_response");
            var data = JsonConvert.DeserializeObject<ServiceDto>(await response.Content.ReadAsStringAsync());
            _context.Set(data, "created_service_response_data");
            _context.Set(data.Id, "service_id");
        }

        [When(@"I make a PUT request in order to update an existing service")]
        public async Task WhenIMakeAPUTRequestInOrderToUpdateAnExistingService()
        {
            using var httpClient = GetHttpClient();

            var createdService = _context.Get<ServiceDto>("created_service_response_data");

            var updatedProgramRequestData = new UpdateServiceRequest
            {
                Id = createdService.Id,
                Name = createdService.Name + "updated",

                Description = createdService.Description,
                LastModified = createdService.LastModified
            };

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/administration/api/Services/{createdService.Id}"),
                Content = JsonContent.Create(updatedProgramRequestData)
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetAdminTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);
            _context.Set(updatedProgramRequestData, "updated_service_request_data");
            _context.Set(response, "updated_service_response");
        }

        [When(@"I make a Delete request in order to delete an existing service")]
        public async Task WhenIMakeADeleteRequestInOrderToDeleteAnExistingService()
        {
            var createdService = _context.Get<ServiceDto>("created_service_response_data");
            using var httpClient = GetHttpClient();
           
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/administration/api/Services/{createdService.Id}")
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetAdminTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);
            _context.Set(response, "deleted_service_response");
        }

        [Then(@"the response status code of delete  is '([^']*)' and the service response data are valid")]
        public void ThenTheResponseStatusCodeOfDeleteIsAndTheResponseDataAreValid(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("deleted_service_response");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.NoContent}, but got {response.StatusCode}.");
            }
        }

        [When(@"I make a Get by id request in order to get an existing service")]
        public async Task WhenIMakeAGetByIdRequestInOrderToGetAnExistingService()
        {
            var createdService = _context.Get<ServiceDto>("created_service_response_data");
            using var httpClient = GetHttpClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{ConfigProvider.GetApiGatewayUrl()}/administration/api/Services/{createdService.Id}")
            };

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetAdminTokenAsync());

            var response = await httpClient.SendAsync(httpRequestMessage);
            _context.Set(response, "get_by_id_service_response");
        }

        [Then(@"the response status code of update is '([^']*)' and the service response data are valid")]
        public async Task ThenTheResponseStatusOfUpdateCodeIsAndTheResponseDataAreValid(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("updated_service_response");
            var requestData = _context.Get<UpdateServiceRequest>("updated_service_request_data");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.OK}, but got {response.StatusCode}.");
            }

            var responseData = JsonConvert.DeserializeObject<ServiceDto>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(responseData.Id);
            Assert.AreEqual(requestData.Name, responseData.Name);

            _context.Set(responseData, "updated_service_response_data");
            _context.Set(responseData.Id, "service_id");
        }

        [Then(@"the response status code of get is '([^']*)' and the service response data are valid")]
        public async Task ThenTheResponseStatusCodeOfGetIsAndTheResponseDataAreValid(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("get_by_id_service_response");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.OK}, but got {response.StatusCode}.");
            }

            var responseData = JsonConvert.DeserializeObject<ServiceDto>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(responseData.Id);
            Assert.IsNotNull(responseData.Name);
            _context.Set(responseData.Id, "service_id");
        }

        [Then(@"the response status code of get is '([^']*)' and the get service by id response data are valid")]
        public async Task ThenTheResponseStatusCodeOfGetIsAndTheResponseDataAreValid1(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("get_by_id_service_response");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.OK}, but got {response.StatusCode}.");
            }

            var responseData = JsonConvert.DeserializeObject<ServiceDto>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(responseData.Id);
            Assert.IsNotNull(responseData.Name);
            _context.Set(responseData.Id, "service_id");
        }

        private HttpClient GetHttpClient()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            return new HttpClient(clientHandler);
        }
    }
}
