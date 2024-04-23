using AutoMapper;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.AspNetCore.Mvc;
using ReimbursementPoC.Vendor.IntergrationEvents;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReimbursementPoC.Vendor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorSubmissionsController : Controller
    {
        #region Fields

        private readonly ILogger<VendorSubmissionsController> _logger;

        #endregion

        #region Constructor

        public VendorSubmissionsController(
            ILogger<VendorSubmissionsController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Actions 

        /// <summary>
        /// Used to get all VendorSubmissions
        /// </summary>
        /// <param name="offset">The page offset.
        ///      <p>Wiil be an integer, an example would be:0</p>
        /// </param>
        /// <param name="limit">The page limit.
        ///      <p>Wiil be an integer, an example would be:50</p>
        /// </param>
        /// <returns>Returns an <see cref="PaginatedList<VendorSubmissionDto>"/>.</returns>
        [HttpGet("active")]
        [SwaggerOperation(Tags = new[] { "VendorSubmission" }, Summary = "Get all VendorSubmissions.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(PaginatedList<VendorSubmissionDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetSubmissionsdAsync(
            [FromQuery] int offset = 0, 
            [FromQuery] int limit = 50,
            [FromQuery] string vendor = null,
            [FromQuery] string service = null,
            [FromQuery] string program = null)
        {

            var client = new ElasticsearchClient(new Uri($"http://{Environment.GetEnvironmentVariable("ElasticSearchHost") ?? "localhost"}:9200"));

            // create index
            var indexName = "vendor_submission_index";

            //// Searching documents
            var response = await client.SearchAsync<VendorSubmissionCreatedIntegrationEvent>(s => GetQuery(indexName, s, offset, limit, vendor, service, program));

            return Ok(response?.Documents);
        }        

        #endregion  

        private static SearchRequestDescriptor<VendorSubmissionCreatedIntegrationEvent> GetQuery(
            string indexName,
            SearchRequestDescriptor<VendorSubmissionCreatedIntegrationEvent> requestDescriptor,
            int offset,
            int limit,
            string vendor,
            string service,
            string program)
        {
                 requestDescriptor
                   .Index(indexName)
                   .From(offset)
                   .Size(limit);

            if (!string.IsNullOrEmpty(vendor))
            {
                requestDescriptor
                .Query(q => q
                   .Term(new TermQuery(new Field("Vendor.Name")) { Value = vendor }));
            }

            if (!string.IsNullOrEmpty(service))
            {
                requestDescriptor
                .Query(q => q
                   .Term(new TermQuery(new Field("Service.Name")) { Value = service }));
            }

            if (!string.IsNullOrEmpty(vendor))
            {
                requestDescriptor
                .Query(q => q
                   .Term(new TermQuery(new Field("Program.Service.Name")) { Value = program }));
            }

            return requestDescriptor;
        }
    }
}