using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReimbursementPoC.Program.API.Models;
using ReimbursementPoC.Program.Application.Services.Commands.CreateService;
using ReimbursementPoC.Program.Application.Services.Commands.DeactivateService;
using ReimbursementPoC.Program.Application.Services.Commands.DeleteService;
using ReimbursementPoC.Program.Application.Services.Commands.UpdateService;
using ReimbursementPoC.Program.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Program.Application.Services.Queries.GetServices;

namespace ReimbursementPoC.Program.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<ServicesController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public ServicesController(
            IMediator mediator,
            ILogger<ServicesController> logger,
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region Actions

        [HttpGet]
        //[SwaggerOperation(Tags = new[] { "Proposal" }, Summary = "Get all Proposals.")]
        //[Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(IEnumerable<ProposalDto>))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetAsync([FromQuery] string? name, [FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            var query = new GetServicesQuery(name, offset, limit);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets a specific Program  by the supplied definition Id.
        /// </summary>
        /// <param name="id">System generated ID returned when create a Program.</param>
        /// <returns>
        /// A <see cref="ServiceDto" /> which matches the input id.
        /// </returns>
        [HttpGet("{id}")]
        // [SwaggerOperation(Tags = new[] { "Program" }, Summary = "Get Program by id.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProgramDto))] 
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "RProgram does not exist")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetServiceByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        //[SwaggerOperation(Tags = new[] { "Program" }, Summary = "Create Program.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProgramDto))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status409Conflict, "Program already exist")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> PostAsync([FromBody] CreateServiceRequest request)
        {
            var result = await _mediator.Send(_mapper.Map<CreateServiceCommand>(request));

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        // [SwaggerOperation(Tags = new[] { "Product" }, Summary = "Update product.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProductDto))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Product does not exist")]
        //[SwaggerResponse(StatusCodes.Status409Conflict, "Product has been updated by someone else")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdateServiceRequest request)
        {
            var result = await _mediator.Send(_mapper.Map<UpdateServiceCommand>(request));
            return Ok(result);
        }

        [HttpPut("{id}/deactivate")]
        // [SwaggerOperation(Tags = new[] { "Product" }, Summary = "Update product.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProductDto))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Product does not exist")]
        //[SwaggerResponse(StatusCodes.Status409Conflict, "Product has been updated by someone else")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> DeactivateAsync(Guid id, [FromBody] DeactivateServiceRequest request)
        {
            var result = await _mediator.Send(new DeactivateServiceCommand { Id = id, LastModified = request.LastModified });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        //[SwaggerOperation(Tags = new[] { "Proposal" }, Summary = "Delete Proposal by id.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status204NoContent)]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Proposal does not exist")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteServiceCommand { Id = id };

            await _mediator.Send(command);
            return NoContent();
        }

        #endregion
    }
}
