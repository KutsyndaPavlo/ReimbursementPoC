using AutoMapper;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Program.Domain.Program;

namespace ReimbursementPoC.Program.Application.Common.Mappings
{

    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
                config.CreateMap<ProgramEntity, ProgramDto>().ReverseMap();
                //config.CreateMap<SellerEntity, SellerDto>().ReverseMap();
                //config.CreateMap<ProposalEntity, ProposalDto>()
                //.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Price))
                //.ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Price.Currency));
                //config.CreateMap<ProposalDto, ProposalEntity>()
                //.ForMember(dest => dest.Price, opt => opt.MapFrom(src => new MoneyValue(src.Price, src.Currency)));
            };
    }
}