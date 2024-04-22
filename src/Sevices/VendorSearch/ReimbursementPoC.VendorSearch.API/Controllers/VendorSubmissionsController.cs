using AutoMapper;
using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public VendorSubmissionsController(
            ILogger<VendorSubmissionsController> logger,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
        public async Task<IActionResult> GetSubmissionsdAsync([FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {

            var client = new ElasticsearchClient(new Uri($"{Environment.GetEnvironmentVariable("ElasticSearchHost") ?? "localhost"}:9200"));

            // create index
            var indexName = "vendor_submission_index";


            //// Searching documents
            var response = await client.SearchAsync<object>(s => s
                .Index(indexName)
                .From(0)
                .Size(10)
            //.Query(q => q
            //    .Term(t => t.User, "flobernd")
            );

            //var query = new GetVendorSubmissionsQuery(offset, limit);
            //var result = await _mediator.Send(query);
            return Ok(response.Hits);
        }

        #endregion

        private Guid GetVendorId()
        {
            return HttpContext.Request.Headers.Keys.Contains("X-UserId")
                ? Guid.Parse(HttpContext.Request.Headers["X-UserId"])
                : Guid.Empty;
        }
    }
}