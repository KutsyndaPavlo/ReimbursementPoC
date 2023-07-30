using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http.Json;

namespace EndToEndTests.StepDefinitions
{
    public class CreateServiceRequest
    {
        public Guid ProgramId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }
    }

    public class UpdateProgramRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int StateId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime LastModified { get; set; }
    }

    public class ProgramDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string State { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }

    public class CreateProgramRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public int StateId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    [Binding]
    public class ProgramStepDefinitions
    {
        private readonly ScenarioContext _context;

        public ProgramStepDefinitions(ScenarioContext context)
        {
            _context = context;
        }

        [Given(@"I am a client")]
        public void GivenIAmAClient()
        {
        }

        [When(@"I make a POST request in order to create a program")]
        public async Task WhenIMakeAPOSTRequestInOrderToCreateAProgram()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };
            var requestData = new CreateProgramRequest
            {
                Name = "Program" + Guid.NewGuid(),
                Description = "",

                StateId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(365)
            };

            var response = await httpClient.PostAsync("program/api/Programs", JsonContent.Create(requestData));
            _context.Set(requestData, "created_program_request_data");
            _context.Set(response, "created_program_response");
        }

        [Then(@"the response status code is '([^']*)' and the response data are valid")]
        public async Task ThenTheResponseStatusCodeIsAndTheResponseDataAreValid(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("created_program_response");
            var requestData = _context.Get<CreateProgramRequest>("created_program_request_data");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.Created}, but got {response.StatusCode}.");
            }

            var responseData = JsonConvert.DeserializeObject<ProgramDto>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(responseData.Id);
            Assert.AreEqual(requestData.Name, responseData.Name);

            _context.Set(responseData, "created_program_response_data");
            _context.Set(responseData.Id, "program_id");
        }

        [Then(@"Program was deleted")]
        public async Task ThenProgramWasDeleted()
        {
            var id = _context.Get<Guid>("program_id");

            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };

            var response = await httpClient.DeleteAsync($"program/api/Programs/{id}");
        }

        [Given(@"Program was created")]
        public async Task GivenProgramWasCreated()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };
            var requestData = new CreateProgramRequest
            {
                Name = "Program" + Guid.NewGuid(),
                Description = "",

                StateId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(365)
            };

            var response = await httpClient.PostAsync("program/api/Programs", JsonContent.Create(requestData));
            _context.Set(requestData, "created_program_request_data");
            _context.Set(response, "created_program_response");
            _context.Set(JsonConvert.DeserializeObject<ProgramDto>(await response.Content.ReadAsStringAsync()), "created_program_response_data");
        }

        [When(@"I make a PUT request in order to update an existing program")]
        public async Task WhenIMakeAPUTRequestInOrderToUpdateAProgram()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };
            var createdProgram = _context.Get<ProgramDto>("created_program_response_data");

            var updatedProgramRequestData = new UpdateProgramRequest
            {
                Id = createdProgram.Id,
                Name = createdProgram.Name + "updated",
                Description = createdProgram.Description,
                StateId = 1,
                StartDate = createdProgram.StartDate,
                EndDate = createdProgram.EndDate,
                LastModified = createdProgram.LastModified
            };

            var response = await httpClient.PutAsync($"program/api/Programs/{createdProgram.Id}", JsonContent.Create(updatedProgramRequestData));
            _context.Set(updatedProgramRequestData, "updated_program_request_data");
            _context.Set(response, "updated_program_response");
        }

        [Then(@"the response status code of update is '([^']*)' and the response data are valid")]
        public async Task ThenTheResponseStatusOfUpdateCodeIsAndTheResponseDataAreValid(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("updated_program_response");
            var requestData = _context.Get<UpdateProgramRequest>("updated_program_request_data");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.OK}, but got {response.StatusCode}.");
            }

            var responseData = JsonConvert.DeserializeObject<ProgramDto>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(responseData.Id);
            Assert.AreEqual(requestData.Name, responseData.Name);

            _context.Set(responseData, "updated_program_response_data");
            _context.Set(responseData.Id, "program_id");
        }

        [When(@"I make a Delete request in order to delete an existing program")]
        public async Task WhenIMakeADeleteRequestInOrderToDeleeteAnExistingProgram()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };
            var createdProgram = _context.Get<ProgramDto>("created_program_response_data");

            var response = await httpClient.DeleteAsync($"program/api/Programs/{createdProgram.Id}");
            _context.Set(response, "deleted_program_response");
        }

        [Then(@"the response status code of delete  is '([^']*)' and the response data are valid")]
        public void ThenTheResponseStatusCodeOfDeleteIsAndTheResponseDataAreValid(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("deleted_program_response");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.NoContent}, but got {response.StatusCode}.");
            }
        }

        [When(@"I make a Get by id request in order to get an existing program")]
        public async Task WhenIMakeAGetByIdRequestInOrderToGetAnExistingProgram()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7120")
            };
            var createdProgram = _context.Get<ProgramDto>("created_program_response_data");

            var response = await httpClient.GetAsync($"program/api/Programs/{createdProgram.Id}");
            _context.Set(response, "get_by_id_program_response");
        }

        [Then(@"the response status code of get is '([^']*)' and the response data are valid")]
        public async Task ThenTheResponseStatusCodeOfGetIsAndTheResponseDataAreValid(int statusCode)
        {
            var response = _context.Get<HttpResponseMessage>("get_by_id_program_response");

            if ((int)response.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.OK}, but got {response.StatusCode}.");
            }

            var responseData = JsonConvert.DeserializeObject<ProgramDto>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(responseData.Id);
            Assert.IsNotNull(responseData.Name);
            _context.Set(responseData.Id, "program_id");
        }
    }
}
