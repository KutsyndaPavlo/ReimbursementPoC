using AutoMapper;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Program.Domain.Product.Enums;
using ReimbursementPoC.Program.Domain.Program;
using ReimbursementPoC.Program.Domain.Program.Enums;
using ReimbursementPoC.Program.Domain.ValueObjects;

namespace ReimbursementPoC.Program.Application.Common.Mappings
{

    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
                config.CreateMap<ProgramEntity, ProgramDto>()
                    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Period.StartDate))
                    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Period.EndDate))
                    .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.Name));

                config.CreateMap<ProgramDto, ProgramEntity>()
                    .ForMember(dest => dest.Period, opt => opt.MapFrom(src => new Period(src.StartDate, src.EndDate)))
                    .ForMember(dest => dest.State, opt => opt.MapFrom(src => StateType.FromDisplayName<StateType>(src.State)));
            };
    }
}