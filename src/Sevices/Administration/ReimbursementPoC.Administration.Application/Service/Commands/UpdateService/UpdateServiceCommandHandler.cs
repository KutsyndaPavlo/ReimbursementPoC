using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Service.Errors;
using ReimbursementPoC.Administration.Domain.Service.Specifications;

namespace ReimbursementPoC.Administration.Application.Services.Commands.UpdateService
{
    internal class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, Result<ServiceDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public UpdateServiceCommandHandler(
            IApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<Result<ServiceDto>> Handle(UpdateServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await _applicationDbContext.Services.Include(x => x.Program).FirstOrDefaultAsync(new ServiceByIdSpecification(command.Id).ToExpression());

            if (service == null)
            {
                return Result<ServiceDto>.Failure(ServiceErrors.NotFound(command.Id));
            }

            if (command.LastModified.Ticks != service.LastModified.Ticks)
            {
                return Result<ServiceDto>.Failure(ServiceErrors.ConcurrentUpdate(command.Id));
            }

            service.Update(
                command.Name,
                command.Description);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ServiceDto>(service);
            return Result<ServiceDto>.Success(dto);
        }
    }
}
