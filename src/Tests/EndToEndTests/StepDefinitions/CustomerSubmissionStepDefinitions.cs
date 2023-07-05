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
    }

    [Binding]
    public class CustomerSubmissionStepDefinitions
    {
        private readonly ScenarioContext _context;

        public CustomerSubmissionStepDefinitions(ScenarioContext context)
        {
            _context = context;
        }

        [Given(@"I am a client")]
        public async Task GivenIAmAClient()
        {

        }

        [Then(@"vendorSubmision was deleted")]
        public async Task ThenVendorSubmisionWasDeleted()
        {
            var id = _context.Get<Guid>("vendorSubmission_id");

            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };

            var response = await httpClient.DeleteAsync($"vendor/api/vendorsubmissions/{id}");
        }

        [Given(@"vendorSubmision was created")]
        public async Task GivenVendorSubmissionWasCreated()
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

        [When(@"I make a POST request in order to create a customerSubmission")]
        public async Task WhenIMakeAPOSTRequestInOrderToCreateAcustomerSubmission()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };
            var requestData = new CreateCustomerSubmissionRequest
            {
                CustomerId = Guid.NewGuid(),
                VendorSubmissionId = _context.Get<Guid>("vendorSubmission_id")
            };

            var response = await httpClient.PostAsync("customer/api/customerSubmissions", JsonContent.Create(requestData));
            var data = JsonConvert.DeserializeObject<CustomerSubmissionDto>(await response.Content.ReadAsStringAsync());

            _context.Set(requestData, "created_customerSubmission_request_data");
            _context.Set(response, "created_customerSubmission_response");
            _context.Set(data, "created_customerSubmissionresponse_data");
            _context.Set(data.Id, "created_customerSubmission_id");
        }

        [Then(@"customerSubmission was deleted")]
        public async Task ThencustomerSubmissionWasDeleted()
        {
            var id = _context.Get<Guid>("created_customerSubmission_id");

            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };

            var response = await httpClient.DeleteAsync($"customer/api/customerSubmissions/{id}");
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

        [Given(@"customerSubmission was created")]
        public async Task GivencustomerSubmissionWasCreated()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };
            var requestData = new CreateCustomerSubmissionRequest
            {
                CustomerId = Guid.NewGuid(),
                VendorSubmissionId = _context.Get<Guid>("vendorSubmission_id")
            };

            var response = await httpClient.PostAsync("customer/api/customerSubmissions", JsonContent.Create(requestData));
            _context.Set(requestData, "created_customerSubmission_request_data");
            _context.Set(response, "created_customerSubmission_response");
            var data = JsonConvert.DeserializeObject<CustomerSubmissionDto>(await response.Content.ReadAsStringAsync());
            _context.Set(data, "created_customerSubmission_response_data");
            _context.Set(data.Id, "customerSubmission_id");
            _context.Set(data.Id, "created_customerSubmission_id");
        }

        [When(@"I make a Delete request in order to delete an existing customerSubmission")]
        public async Task WhenIMakeADeleteRequestInOrderToDeleteAnExistingcustomerSubmission()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };
            var createdcustomerSubmission = _context.Get<CustomerSubmissionDto>("created_customerSubmission_response_data");

            var response = await httpClient.DeleteAsync($"customer/api/customerSubmissions/{createdcustomerSubmission.Id}");
            _context.Set(response, "deleted_customerSubmission_response");
        }

        [Then(@"the response status code of delete  is '([^']*)' and the customerSubmission response data are valid")]
        public void ThenTheResponseStatusCodeOfDeleteIsAndTheResponseDataAreValid(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("deleted_customerSubmission_response");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.NoContent}, but got {response.StatusCode}.");
            }
        }

        [When(@"I make a Get by id request in order to get an existing customerSubmission")]
        public async Task WhenIMakeAGetByIdRequestInOrderToGetAnExistingcustomerSubmission()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };
            var createdcustomerSubmission = _context.Get<CustomerSubmissionDto>("created_customerSubmission_response_data");

            var response = await httpClient.GetAsync($"customer/api/customerSubmissions/{createdcustomerSubmission.Id}");
            _context.Set(response, "get_by_id_customerSubmission_response");
        }

        [Then(@"the response status code of get is '([^']*)' and the customerSubmission response data are valid")]
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

        [Then(@"the response status code of get is '([^']*)' and the get customerSubmission by id response data are valid")]
        public async Task ThenTheResponseStatusCodeOfGetIsAndTheResponseDataAreValid1(int statusCode)
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
