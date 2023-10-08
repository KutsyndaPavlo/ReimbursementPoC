using AutoMapper;
using ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById;
using ReimbursementPoC.Customer.Domain.Customer;

namespace ReimbursementPoC.Customer.Application.Common.Mappings
{

    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
                config.CreateMap<CustomerSubmissionEntity, CustomerSubmissionDto>().ReverseMap();
            };
    }
}