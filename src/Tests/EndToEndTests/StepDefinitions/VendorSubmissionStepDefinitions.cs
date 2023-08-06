using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http.Json;

namespace EndToEndTests.StepDefinitions
{
    public class vendorSubmissionDto
    {
        public Guid Id { get; set; }

        public Guid VendorId { get; set; }

        public Guid ServiceId { get; set; }
    }

    public class CreatevendorSubmissionRequest
    {
        public Guid VendorId { get; set; }

        public Guid ServiceId { get; set; }
    }

    [Binding]
    public class vendorSubmissiontepDefinitions
    {
        private readonly ScenarioContext _context;

        public vendorSubmissiontepDefinitions(ScenarioContext context)
        {
            _context = context;
        }

        [Given(@"I am a client")]
        public async Task GivenIAmAClient()
        {
            
        }

        [Then(@"service  was deleted1")]
        public async Task ThenServiceWasDeleted()
        {
            var id = _context.Get<Guid>("service_id");

            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };

            var response = await httpClient.DeleteAsync($"program/api/services/{id}");
        }

        [Given(@"service  was created1")]
        public async Task GivenServiceWasCreated()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };
            var requestData = new CreateServiceRequest
            {
                Name = "Service" + Guid.NewGuid(),
                Description = "",
                ProgramId = _context.Get<Guid>("program_id")
            };

            var response = await httpClient.PostAsync("Program/api/Services", JsonContent.Create(requestData));
            _context.Set(requestData, "created_service_request_data");
            _context.Set(response, "created_service_response");
            var data = JsonConvert.DeserializeObject<ServiceDto>(await response.Content.ReadAsStringAsync());
            _context.Set(data, "created_service_response_data");
            _context.Set(data.Id, "service_id");
            _context.Set(data.Id, "created_service_id");
        }

        [When(@"I make a POST request in order to create a vendorSubmission")]
        public async Task WhenIMakeAPOSTRequestInOrderToCreateAvendorSubmission()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };
            var requestData = new CreatevendorSubmissionRequest
            {
                VendorId = Guid.NewGuid(),
                ServiceId = _context.Get<Guid>("created_service_id")
            };

            var response = await httpClient.PostAsync("vendor/api/vendorSubmissions", JsonContent.Create(requestData));
            var data = JsonConvert.DeserializeObject<vendorSubmissionDto>(await response.Content.ReadAsStringAsync());

            _context.Set(requestData, "created_vendorSubmission_request_data");
            _context.Set(response, "created_vendorSubmission_response");
            _context.Set(data, "created_vendorSubmissionresponse_data");
            _context.Set(data.Id, "created_vendorSubmission_id");
        }

        [Then(@"vendorSubmission was deleted")]
        public async Task ThenvendorSubmissionWasDeleted()
        {
            var id = _context.Get<Guid>("vendorSubmission_id");

            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };

            var response = await httpClient.DeleteAsync($"vendor/api/vendorSubmissions/{id}");
        }

        [Then(@"the response status code is '([^']*)' and the vendorSubmission response data are valid")]
        public async Task ThenTheResponseStatusCodeIsAndTheResponseDataAreValid(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("created_vendorSubmission_response");
            var requestData = _context.Get<CreatevendorSubmissionRequest>("created_vendorSubmission_request_data");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.Created}, but got {response.StatusCode}.");
            }

            var responseData = JsonConvert.DeserializeObject<vendorSubmissionDto>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(responseData.Id);
            //Assert.AreEqual(requestData.Name, responseData.Name);

            _context.Set(responseData, "created_seervice_response_data");
            _context.Set(responseData.Id, "vendorSubmission_id");
        }

        [Given(@"vendorSubmission was created")]
        public async Task GivenvendorSubmissionWasCreated()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };
            var requestData = new CreatevendorSubmissionRequest
            {
                VendorId = Guid.NewGuid(),
                ServiceId = _context.Get<Guid>("service_id")
            };

            var response = await httpClient.PostAsync("vendor/api/vendorSubmissions", JsonContent.Create(requestData));
            _context.Set(requestData, "created_vendorSubmission_request_data");
            _context.Set(response, "created_vendorSubmission_response");
            var data = JsonConvert.DeserializeObject<vendorSubmissionDto>(await response.Content.ReadAsStringAsync());
            _context.Set(data, "created_vendorSubmission_response_data");
            _context.Set(data.Id, "vendorSubmission_id");
        }

        [When(@"I make a Delete request in order to delete an existing vendorSubmission")]
        public async Task WhenIMakeADeleteRequestInOrderToDeleteAnExistingvendorSubmission()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };
            var createdvendorSubmission = _context.Get<vendorSubmissionDto>("created_vendorSubmission_response_data");

            var response = await httpClient.DeleteAsync($"vendor/api/vendorSubmissions/{createdvendorSubmission.Id}");
            _context.Set(response, "deleted_vendorSubmission_response");
        }

        [Then(@"the response status code of delete  is '([^']*)' and the vendorSubmission response data are valid")]
        public void ThenTheResponseStatusCodeOfDeleteIsAndTheResponseDataAreValid(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("deleted_vendorSubmission_response");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.NoContent}, but got {response.StatusCode}.");
            }
        }

        [When(@"I make a Get by id request in order to get an existing vendorSubmission")]
        public async Task WhenIMakeAGetByIdRequestInOrderToGetAnExistingvendorSubmission()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };
            var createdvendorSubmission = _context.Get<vendorSubmissionDto>("created_vendorSubmission_response_data");

            var response = await httpClient.GetAsync($"vendor/api/vendorSubmissions/{createdvendorSubmission.Id}");
            _context.Set(response, "get_by_id_vendorSubmission_response");
        }

        [Then(@"the response status code of get is '([^']*)' and the vendorSubmission response data are valid")]
        public async Task ThenTheResponseStatusCodeOfGetIsAndTheResponseDataAreValid(int statusCode)
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

        [Then(@"the response status code of get is '([^']*)' and the get vendorSubmission by id response data are valid")]
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
    }
}
