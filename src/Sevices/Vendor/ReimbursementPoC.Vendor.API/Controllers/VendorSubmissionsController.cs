using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReimbursementPoC.Vendor.API.Models;
using ReimbursementPoC.Vendor.Application.Common.Model;
using ReimbursementPoC.Vendor.Application.Vendor.Commands.CancelVendorSubmission;
using ReimbursementPoC.Vendor.Application.Vendor.Commands.CreateVendor;
using ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById;
using ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendors;
using ReimbursementPoC.Vendor.Application.VendorSubmission.Queries.GetVendorSubmissionsByVendorId;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReimbursementPoC.Vendor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorSubmissionsController : Controller
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<VendorSubmissionsController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public VendorSubmissionsController(
            IMediator mediator,
            ILogger<VendorSubmissionsController> logger,
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "VendorSubmission" }, Summary = "Get all VendorSubmissions.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(PaginatedList<VendorSubmissionDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetSubmissionsByVendorIdAsync([FromQuery] string? name, [FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            var rr = HttpContext.Request.Headers["X-UserId"];
            // todo check vendor id

            var query = new GetVendorSubmissionsQuery(offset, limit);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

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
        [HttpGet("{vendorId}/submissions")]
        [SwaggerOperation(Tags = new[] { "VendorSubmission" }, Summary = "Get all VendorSubmissions by Vendor id.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(PaginatedList<VendorSubmissionDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid vendorId, [FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            var query = new GetVendorSubmissionsByVendorIdQuery(offset, limit, vendorId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets a specific Vendor  by the supplied Id.
        /// </summary>
        /// <param name="id">System generated ID returned when create a VendorSubmission.</param>
        /// <returns>
        /// A <see cref="VendorSubmissionDto" /> which matches the input id.
        /// </returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Tags = new[] { "VendorSubmission" }, Summary = "Get VendorSubmission by id.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(VendorSubmissionDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "VendorSubmission does not exist")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> Get(Guid id)
        {
            //ToDo check id Guid.Parse("b2c95337-499f-4aca-b2d2-f8235603b8d1") // ToDo//VendorId 

            var query = new GetVendorSubmissionByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [SwaggerOperation(Tags = new[] { "VendorSubmission" }, Summary = "Create a VendorSubmission.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(VendorSubmissionDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Vendor already exist")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> PostAsync([FromBody] CreateVendorSubmissionRequest request)
        {
            var rr = HttpContext.Request.Headers["X-UserId"];
            // todo check vendor id

            var result = await _mediator.Send(new CreateVendorSubmissionCommand
            {
                ServiceId = request.ServiceId,
                VendorId = request.VendorId,
                ServiceFullName = request.ServiceFullName,
                Description = request.Description
            });

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}/cancel")]
        [SwaggerOperation(Tags = new[] { "VendorSubmission" }, Summary = "Cancel a VendorSubmission.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(VendorSubmissionDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "VendorSubmission does not exist")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> CancelAsync(Guid id)
        {

            var rr = HttpContext.Request.Headers["X-UserId"];
            // todo check vendor id

            var result = await _mediator.Send(new CancelVendorSubmissionCommand
            {
                SubmissionId = id,
                // VendorId = VendorId
            });
            return Ok(result);
        }

        #endregion
    }
}