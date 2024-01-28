using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReimbursementPoC.Customer.API.Models;
using ReimbursementPoC.Customer.Application.Common.Model;
using ReimbursementPoC.Customer.Application.Customer.Commands.CreateCustomer;
using ReimbursementPoC.Customer.Application.Customer.Commands.DeactivateCustomer;
using ReimbursementPoC.Customer.Application.Customer.Commands.DeleteCustomer;
using ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById;
using ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomers;
using ReimbursementPoC.Customer.Application.CustomerSubmission.Queries.GetCustomerSubmissionsByCustomerId;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReimbursementPoC.Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSubmissionsController : Controller
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<CustomerSubmissionsController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public CustomerSubmissionsController(
            IMediator mediator,
            ILogger<CustomerSubmissionsController> logger,
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region Actions 

        /// <summary>
        /// Used to get all CustomerSubmissions
        /// </summary>
        /// <param name="offset">The page offset.
        ///      <p>Wiil be an integer, an example would be:0</p>
        /// </param>
        /// <param name="limit">The page limit.
        ///      <p>Wiil be an integer, an example would be:50</p>
        /// </param>
        /// <returns>Returns an <see cref="PaginatedList<CustomerSubmissionDto>"/>.</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "CustomerSubmission" }, Summary = "Get all CustomerSubmissions.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(PaginatedList<CustomerSubmissionDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetAsync([FromQuery] string? name, [FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            var query = new GetCustomerSubmissionsQuery(offset, limit);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Used to get all CustomerSubmissions
        /// </summary>
        /// <param name="offset">The page offset.
        ///      <p>Wiil be an integer, an example would be:0</p>
        /// </param>
        /// <param name="limit">The page limit.
        ///      <p>Wiil be an integer, an example would be:50</p>
        /// </param>
        /// <returns>Returns an <see cref="PaginatedList<CustomerSubmissionDto>"/>.</returns>
        [HttpGet("{customerId}/submissions")]
        [SwaggerOperation(Tags = new[] { "CustomerSubmission" }, Summary = "Get all CustomerSubmissions.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(PaginatedList<CustomerSubmissionDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetByCustomerIdAsync([FromRoute] Guid customerId, [FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            //if (customerId != GetCustomerId())
            //{
            //    return Forbid();
            //}

            var query = new GetCustomerSubmissionsByCustomerIdQuery(customerId, offset, limit);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets a specific Customer  by the supplied Id.
        /// </summary>
        /// <param name="id">System generated ID returned when create a CustomerSubmission.</param>
        /// <returns>
        /// A <see cref="CustomerSubmissionDto" /> which matches the input id.
        /// </returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Tags = new[] { "CustomerSubmission" }, Summary = "Get CustomerSubmission by id.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(CustomerSubmissionDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "CustomerSubmission does not exist")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetCustomerSubmissionByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [SwaggerOperation(Tags = new[] { "CustomerSubmission" }, Summary = "Create a CustomerSubmission.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(CustomerSubmissionDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Customer already exist")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> PostAsync([FromBody] CreateCustomerSubmissionRequest request)
        {
            //if (request.CustomerId != GetCustomerId())
            //{
            //    return Forbid();
            //}

            var result = await _mediator.Send(_mapper.Map<CreateCustomerSubmissionCommand>(request));

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}/cancel")]
        [SwaggerOperation(Tags = new[] { "CustomerSubmission" }, Summary = "Deactivate a CustomerSubmission.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(CustomerSubmissionDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "CustomerSubmission does not exist")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> CancelAsync(Guid id)
        {
            //if (id != GetCustomerId())
            //{
            //    return Forbid();
            //}

            var result = await _mediator.Send(new CancelCustomerSubmissionCommand { Id = id });
            return Ok(result);
        }

        #endregion

        private Guid GetCustomerId()
        {
            return HttpContext.Request.Headers.Keys.Contains("X-UserId")
                ? Guid.Parse(HttpContext.Request.Headers["X-UserId"])
                : Guid.Empty;
        }
    }
}