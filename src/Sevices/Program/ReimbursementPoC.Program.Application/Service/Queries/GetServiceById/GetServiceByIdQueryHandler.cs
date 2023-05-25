﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Domain.Service.Exeption;
using ReimbursementPoC.Program.Domain.Service.Specifications;

namespace ReimbursementPoC.Program.Application.Services.Queries.GetServiceById
{
    public class GetServiceByIdQueryHandler
        : IRequestHandler<GetServiceByIdQuery, ServiceDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetServiceByIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ServiceDto> Handle(GetServiceByIdQuery query, CancellationToken cancellationToken)
        {
            var service = await _applicationDbContext.Services.FirstOrDefaultAsync(new ServiceByIdSpecification(query.Id).ToExpression());

            if (service == null)
            {
                throw new ServiceNotFoundException($"Service with id {query.Id} doesn't exist");
            }

            return _mapper.Map<ServiceDto>(service);
        }
    }
}
