using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReimbursementPoC.Program.API.Models;
using ReimbursementPoC.Program.Application.Program.Commands.CreateProgram;
using ReimbursementPoC.Program.Application.Program.Commands.DeactivateProgram;
using ReimbursementPoC.Program.Application.Program.Commands.DeleteProgram;
using ReimbursementPoC.Program.Application.Program.Commands.UpdateProgram;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Program.Application.Program.Queries.GetPrograms;
using ReimbursementPoC.Program.Application.Services.Commands.DeleteService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReimbursementPoC.Program.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController : Controller
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<ProgramsController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public ProgramsController(
            IMediator mediator,
            ILogger<ProgramsController> logger,
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region Actions 

        [HttpGet]
        //[SwaggerOperation(Tags = new[] { "Program" }, Summary = "Get all Programs.")]
        //[Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(IEnumerable<ProgramDto>))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetAsync([FromQuery] string? name, [FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            var query = new GetProgramsQuery(name, offset, limit);
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
            var query = new GetProgramByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        ///// <summary>
        ///// Gets a specific Program  by the supplied definition Id.
        ///// </summary>
        ///// <param name="id">System generated ID returned when create a Program.</param>
        ///// <returns>
        ///// A <see cref="ProgramDto" /> which matches the input id.
        ///// </returns>
        //[HttpGet("{id}/proposals")]
        //// [SwaggerOperation(Tags = new[] { "Program" }, Summary = "Get Program by id.")]
        //[Produces("application/json")]
        ////[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProgramDto))] 
        ////[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        ////[SwaggerResponse(StatusCodes.Status404NotFound, "RProgram does not exist")]
        ////[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        //public async Task<IActionResult> GetProposals(Guid id)
        //{
        //    var query = new GetProgramProposalsQuery(id);
        //    var result = await _mediator.Send(query);

        //    if (result == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}

        [HttpPost]
        //[SwaggerOperation(Tags = new[] { "Program" }, Summary = "Create Program.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProgramDto))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status409Conflict, "Program already exist")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> PostAsync([FromBody] CreateProgramRequest request)
        {
            var result = await _mediator.Send(_mapper.Map<CreateProgramCommand>(request));

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        // [SwaggerOperation(Tags = new[] { "Program" }, Summary = "Update Program.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProgramDto))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Program does not exist")]
        //[SwaggerResponse(StatusCodes.Status409Conflict, "Program has been updated by someone else")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdateProgramRequest request)
        {
            var result = await _mediator.Send(_mapper.Map<UpdateProgramCommand>(request));
            return Ok(result);
        }

        [HttpPut("{id}/deactivate")]
        // [SwaggerOperation(Tags = new[] { "Program" }, Summary = "Update Program.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProgramDto))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Program does not exist")]
        //[SwaggerResponse(StatusCodes.Status409Conflict, "Program has been updated by someone else")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> DeactivateAsync(Guid id, [FromBody] DeactivateProgramRequest request)
        {
            var result = await _mediator.Send(new DeactivateProgramCommand { Id = id, LastModified = request.LastModified });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        //[SwaggerOperation(Tags = new[] { "Program" }, Summary = "Delete Program by id.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status204NoContent)]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Program does not exist")]
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