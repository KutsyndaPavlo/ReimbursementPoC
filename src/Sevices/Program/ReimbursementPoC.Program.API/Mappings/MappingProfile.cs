using AutoMapper;
using ReimbursementPoC.Program.API.Models;
using ReimbursementPoC.Program.Application.Program.Commands.CreateProgram;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Program.Application.Program.Queries.GetPrograms;
using ReimbursementPoC.Program.Application.Services.Commands.CreateService;

namespace ReimbursementPoC.Program.API.Mappings
{
    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
                //config.CreateMap<GetProgramsQuery, ReimbursementPoC.Program.Application.Program.Queries.GetPrograms.GetProgramsQuery>().ReverseMap();
                //config.CreateMap<GetProgramByIdQuery, ReimbursementPoC.Program.Application.Program.Queries.GetProgramById.GetProgramByIdQuery>().ReverseMap();
                //config.CreateMap<PriceAnalytics.Administration.Services.Program.Page, ReimbursementPoC.Program.Application.Common.Model.Page>().ReverseMap();
                //config.CreateMap<ProgramPaginatedList, PaginatedList<ReimbursementPoC.Program.Application.Program.Queries.GetProgramById.ProgramDto>>().ReverseMap();// ToDo
                config.CreateMap<CreateProgramRequest, ReimbursementPoC.Program.Application.Program.Commands.CreateProgram.CreateProgramCommand>().ReverseMap();
                config.CreateMap<UpdateProgramRequest, ReimbursementPoC.Program.Application.Program.Commands.UpdateProgram.UpdateProgramCommand>().ReverseMap();
                config.CreateMap<CreateServiceRequest, CreateServiceCommand>().ReverseMap();
               // config.CreateMap<UpdateServiceRequest, UpdateServiceCommand>().ReverseMap();
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                //config.CreateMap<DeactivateProgramCommand, ReimbursementPoC.Program.Application.Program.Commands.DeactivateProgram.DeactivateProgramCommand>()
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                //config.CreateMap<ReimbursementPoC.Program.Application.Program.Commands.UpdateProgram.UpdateProgramCommand, UpdateProgramCommand>()
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                //config.CreateMap<DeleteProgramCommand, ReimbursementPoC.Program.Application.Program.Commands.DeleteProgram.DeleteProgramCommand>().ReverseMap();
                //config.CreateMap<ReimbursementPoC.Program.Application.Program.Queries.GetProgramById.ProgramDto, ProgramDto>()
                //.ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<ProgramDto, ReimbursementPoC.Program.Application.Program.Queries.GetProgramById.ProgramDto>();
                //.ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));

                // config.CreateMap<CreateProposalCommand, ReimbursementPoC.Program.Application.Proposal.Commands.CreateProposal.CreateProposalCommand>().ReverseMap();
                // config.CreateMap<ProposalDto, ReimbursementPoC.Program.Application.Proposal.Queries.GetProposalById.ProposalDto>()
                // .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<ReimbursementPoC.Program.Application.Proposal.Queries.GetProposalById.ProposalDto, ProposalDto>()
                //.ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<GetProposalsQuery, ReimbursementPoC.Program.Application.Proposal.Queries.GetProposals.GetProposalsQuery>().ReverseMap();
                // config.CreateMap<GetProposalByIdQuery, ReimbursementPoC.Program.Application.Proposal.Queries.GetProposalById.GetProposalByIdQuery>().ReverseMap();
                // config.CreateMap<PriceAnalytics.Administration.Services.Proposal.Page, ReimbursementPoC.Program.Application.Common.Model.Page>();
                // config.CreateMap<DeleteProposalCommand, ReimbursementPoC.Program.Application.Proposal.Commands.DeleteProposal.DeleteProposalCommand>().ReverseMap();
                // config.CreateMap<ProposalPaginatedList, PaginatedList<ReimbursementPoC.Program.Application.Proposal.Queries.GetProposalById.ProposalDto>>().ReverseMap();// ToDo
                // config.CreateMap<PriceAnalytics.Administration.Services.Proposal.Page, ReimbursementPoC.Program.Application.Common.Model.Page>().ReverseMap();

                // config.CreateMap<GetSellersQuery, ReimbursementPoC.Program.Application.Seller.Queries.GetSellers.GetSellersQuery>().ReverseMap();
                // config.CreateMap<GetSellerByIdQuery, ReimbursementPoC.Program.Application.Seller.Queries.GetSellerById.GetSellerByIdQuery>().ReverseMap();
                // config.CreateMap<PriceAnalytics.Administration.Services.Seller.Page, ReimbursementPoC.Program.Application.Common.Model.Page>();
                // config.CreateMap<SellerPaginatedList, PaginatedList<ReimbursementPoC.Program.Application.Seller.Queries.GetSellerById.SellerDto>>().ReverseMap();// ToDo
                // config.CreateMap<CreateSellerCommand, ReimbursementPoC.Program.Application.Product.Commands.CreateSeller.CreateSellerCommand>().ReverseMap();
                // config.CreateMap<UpdateSellerCommand, ReimbursementPoC.Program.Application.Seller.Commands.UpdateSeller.UpdateSellerCommand>()
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<DeactivateSellerCommand, ReimbursementPoC.Program.Application.Seller.Commands.DeactivateSeller.DeactivateSellerCommand>()
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<ReimbursementPoC.Program.Application.Seller.Commands.UpdateSeller.UpdateSellerCommand, UpdateSellerCommand>()
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<DeleteSellerCommand, ReimbursementPoC.Program.Application.Seller.Commands.DeleteSeller.DeleteSellerCommand>().ReverseMap();
                // config.CreateMap<ReimbursementPoC.Program.Application.Seller.Queries.GetSellerById.SellerDto, SellerDto>()
                // .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<SellerDto, ReimbursementPoC.Program.Application.Seller.Queries.GetSellerById.SellerDto>()
                // .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<PriceAnalytics.Administration.Services.Seller.Page, ReimbursementPoC.Program.Application.Common.Model.Page>().ReverseMap();
            };
    }
}
