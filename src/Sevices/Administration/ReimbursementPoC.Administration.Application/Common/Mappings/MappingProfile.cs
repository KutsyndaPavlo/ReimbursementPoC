using AutoMapper;
using ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Administration.Domain.Program;
using ReimbursementPoC.Administration.Domain.Service;
using ReimbursementPoC.Administration.Domain.ValueObjects;

namespace ReimbursementPoC.Administration.Application.Common.Mappings
{

    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
                config.CreateMap<ServiceEntity, ServiceDto>().ReverseMap();

                config.CreateMap<ProgramEntity, ProgramDto>()
                    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Period.StartDate))
                    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Period.EndDate))
                    .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.State.Id))
                    .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.Name));

                config.CreateMap<ProgramDto, ProgramEntity>()
                    .ForMember(dest => dest.Period, opt => opt.MapFrom(src => new Period(src.StartDate, src.EndDate)));
            };
    }
}