﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReimbursementPoC.Administration.API.Models;
using ReimbursementPoC.Administration.Application.Common.Model;
using ReimbursementPoC.Administration.Application.Service.Queries.GetServicesByProgramId;
using ReimbursementPoC.Administration.Application.Services.Commands.CreateService;
using ReimbursementPoC.Administration.Application.Services.Commands.DeactivateService;
using ReimbursementPoC.Administration.Application.Services.Commands.DeleteService;
using ReimbursementPoC.Administration.Application.Services.Commands.UpdateService;
using ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Administration.Application.Services.Queries.GetServices;
using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Program.Errors;
using ReimbursementPoC.Administration.Domain.Service.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace ReimbursementPoC.service.API.Controllers
{
    [Route("api")]
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

        [HttpGet("services/active")]
        [SwaggerOperation(Tags = new[] { "service" }, Summary = "Get active services")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(PaginatedList<ServiceDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetServicesByProgramIdAsync([FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            var query = new GetActiveServicesQuery(offset, limit);
            var result = await _mediator.Send(query);

            return result.IsSuccess
                ? Ok(result.Data)
                : BadRequest(result.Error);
        }

        [HttpGet("programs/{programId}/services")]
        [SwaggerOperation(Tags = new[] { "service" }, Summary = "Get services by program id.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(PaginatedList<ServiceDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetServicesByProgramIdAsync([FromRoute] Guid programId, [FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            var query = new GetServicesByProgramIdQuery(programId, offset, limit);
            var result = await _mediator.Send(query);

            return result.IsSuccess
                ? Ok(result.Data)
                : BadRequest(result.Error);
        }

        [HttpGet("programs/{programId}/services/{id}", Name = "GetByIdAsync")]
        [SwaggerOperation(Tags = new[] { "Service" }, Summary = "Get service by id.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ServiceDto))] 
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Service does not exist")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid programId, Guid id)
        {
            var query = new GetServiceByIdQuery(id);
            var result = await _mediator.Send(query);

            return result.IsSuccess
                ? Ok(result.Data)
                : result.Error.Code == ProgramErrors.NotFoundCode
                    ? NotFound(result.Error)
                    : BadRequest(result.Error);
        }

        [HttpPost("programs/{programId}/services")]
        [SwaggerOperation(Tags = new[] { "Service" }, Summary = "Create a service.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ServiceDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Service already exist")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> PostServiceAsync([FromRoute] Guid programId, [FromBody] CreateServiceRequest request)
        {
            var command = _mapper.Map<CreateServiceCommand>(request);
            command.ProgramId = programId;

            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? CreatedAtRoute(nameof(GetByIdAsync), new { programId, id = result.Data.Id }, result.Data)
                : BadRequest(result.Error);
        }

        [HttpPut("programs/{programId}/services/{id}")]
        [SwaggerOperation(Tags = new[] { "Service" }, Summary = "Update a service.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ServiceDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Service does not exist")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Service has been updated by someone else")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid programId, Guid id, [FromBody] UpdateServiceRequest request)
        {
            var result = await _mediator.Send(_mapper.Map<UpdateServiceCommand>(request));

            return result.IsSuccess
                ? Ok(result.Data)
                : result.Error.Code == ServiceErrors.ConcurrentUpdateCode
                    ? Conflict(result.Error)
                    : BadRequest(result.Error);
        }

        [HttpPut("programs/{programId}/services/{id}/cancel")]
        [SwaggerOperation(Tags = new[] { "Service" }, Summary = "Cancel a service.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ServiceDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Service does not exist")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Service has been updated by someone else")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> CancelServiceAsync([FromRoute] Guid programId,Guid id)
        {
            var result = await _mediator.Send(new CancelServiceCommand
            {
                Id = id
            });

            return result.IsSuccess
                ? Ok(result.Data)
                : BadRequest(result.Error);
        }

        [HttpDelete("programs/{programId}/services/{id}")]
        [SwaggerOperation(Tags = new[] { "service" }, Summary = "Delete service by id.")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Service does not exist")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> DeleteServiceAsync([FromRoute] Guid programId,Guid id)
        {
            var command = new DeleteServiceCommand(id);

            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Error);
        }

        #endregion
    }
}
