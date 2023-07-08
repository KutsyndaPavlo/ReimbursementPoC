using AutoMapper;
using ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById;
using ReimbursementPoC.Vendor.Domain.Vendor;

namespace ReimbursementPoC.Vendor.Application.Common.Mappings
{

    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
                config.CreateMap<VendorSubmissionEntity, VendorSubmissionDto>().ReverseMap();
            };
    }
}