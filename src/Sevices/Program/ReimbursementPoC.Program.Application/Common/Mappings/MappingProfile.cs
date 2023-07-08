using AutoMapper;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Program.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Program.Domain.Program;
using ReimbursementPoC.Program.Domain.Service;
using ReimbursementPoC.Program.Domain.ValueObjects;

namespace ReimbursementPoC.Program.Application.Common.Mappings
{

    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
                config.CreateMap<ServiceEntity, ServiceDto>().ReverseMap();

                config.CreateMap<ProgramEntity, ProgramDto>()
                    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Period.StartDate))
                    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Period.EndDate));

                config.CreateMap<ProgramEntity, ProgramFullDto>()
                    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Period.StartDate))
                    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Period.EndDate));
                    //.ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services.ToList()));

                config.CreateMap<ProgramDto, ProgramEntity>()
                    .ForMember(dest => dest.Period, opt => opt.MapFrom(src => new Period(src.StartDate, src.EndDate)));

                config.CreateMap<ProgramFullDto, ProgramEntity>()
                    .ForMember(dest => dest.Period, opt => opt.MapFrom(src => new Period(src.StartDate, src.EndDate)));                
            };
    }
}