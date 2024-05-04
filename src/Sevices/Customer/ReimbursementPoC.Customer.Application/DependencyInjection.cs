using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReimbursementPoC.Customer.Application.Common.Behaviours;
using ReimbursementPoC.Customer.Application.Common.Mappings;
using ReimbursementPoC.Customer.Application.CustomerSubmission.DomainServices;
using ReimbursementPoC.Customer.Domain.CustomerSubmission.DomainServices;
using System.Reflection;

namespace ReimbursementPoC.Customer.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(MappingProfile.AutoMapperConfig, typeof(MappingProfile).Assembly);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped<ICustomerSubmissionService, CustomerSubmissionService>();

            return services;
        }
    }
}