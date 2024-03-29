﻿using AutoMapper;
using ReimbursementPoC.Administration.API.Models;
using ReimbursementPoC.Administration.Application.Program.Commands.CreateProgram;
using ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Administration.Application.Program.Queries.GetPrograms;
using ReimbursementPoC.Administration.Application.Services.Commands.CreateService;
using ReimbursementPoC.Administration.Application.Services.Commands.UpdateService;

namespace ReimbursementPoC.Administration.API.Mappings
{
    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
                //config.CreateMap<GetProgramsQuery, ReimbursementPoC.Administration.Application.Program.Queries.GetPrograms.GetProgramsQuery>().ReverseMap();
                //config.CreateMap<GetProgramByIdQuery, ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById.GetProgramByIdQuery>().ReverseMap();
                //config.CreateMap<PriceAnalytics.Administration.Services.Program.Page, ReimbursementPoC.Administration.Application.Common.Model.Page>().ReverseMap();
                //config.CreateMap<ProgramPaginatedList, PaginatedList<ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById.ProgramDto>>().ReverseMap();// ToDo
                config.CreateMap<CreateProgramRequest, ReimbursementPoC.Administration.Application.Program.Commands.CreateProgram.CreateProgramCommand>().ReverseMap();
                config.CreateMap<UpdateProgramRequest, ReimbursementPoC.Administration.Application.Program.Commands.UpdateProgram.UpdateProgramCommand>().ReverseMap();
                config.CreateMap<CreateServiceRequest, CreateServiceCommand>().ReverseMap();
                config.CreateMap<UpdateServiceRequest, UpdateServiceCommand>().ReverseMap();
                // config.CreateMap<UpdateServiceRequest, UpdateServiceCommand>().ReverseMap();
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                //config.CreateMap<DeactivateProgramCommand, ReimbursementPoC.Administration.Application.Program.Commands.DeactivateProgram.DeactivateProgramCommand>()
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                //config.CreateMap<ReimbursementPoC.Administration.Application.Program.Commands.UpdateProgram.UpdateProgramCommand, UpdateProgramCommand>()
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                //config.CreateMap<DeleteProgramCommand, ReimbursementPoC.Administration.Application.Program.Commands.DeleteProgram.DeleteProgramCommand>().ReverseMap();
                //config.CreateMap<ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById.ProgramDto, ProgramDto>()
                //.ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<ProgramDto, ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById.ProgramDto>();
                //.ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));

                // config.CreateMap<CreateProposalCommand, ReimbursementPoC.Administration.Application.Proposal.Commands.CreateProposal.CreateProposalCommand>().ReverseMap();
                // config.CreateMap<ProposalDto, ReimbursementPoC.Administration.Application.Proposal.Queries.GetProposalById.ProposalDto>()
                // .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<ReimbursementPoC.Administration.Application.Proposal.Queries.GetProposalById.ProposalDto, ProposalDto>()
                //.ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<GetProposalsQuery, ReimbursementPoC.Administration.Application.Proposal.Queries.GetProposals.GetProposalsQuery>().ReverseMap();
                // config.CreateMap<GetProposalByIdQuery, ReimbursementPoC.Administration.Application.Proposal.Queries.GetProposalById.GetProposalByIdQuery>().ReverseMap();
                // config.CreateMap<PriceAnalytics.Administration.Services.Proposal.Page, ReimbursementPoC.Administration.Application.Common.Model.Page>();
                // config.CreateMap<DeleteProposalCommand, ReimbursementPoC.Administration.Application.Proposal.Commands.DeleteProposal.DeleteProposalCommand>().ReverseMap();
                // config.CreateMap<ProposalPaginatedList, PaginatedList<ReimbursementPoC.Administration.Application.Proposal.Queries.GetProposalById.ProposalDto>>().ReverseMap();// ToDo
                // config.CreateMap<PriceAnalytics.Administration.Services.Proposal.Page, ReimbursementPoC.Administration.Application.Common.Model.Page>().ReverseMap();

                // config.CreateMap<GetSellersQuery, ReimbursementPoC.Administration.Application.Seller.Queries.GetSellers.GetSellersQuery>().ReverseMap();
                // config.CreateMap<GetSellerByIdQuery, ReimbursementPoC.Administration.Application.Seller.Queries.GetSellerById.GetSellerByIdQuery>().ReverseMap();
                // config.CreateMap<PriceAnalytics.Administration.Services.Seller.Page, ReimbursementPoC.Administration.Application.Common.Model.Page>();
                // config.CreateMap<SellerPaginatedList, PaginatedList<ReimbursementPoC.Administration.Application.Seller.Queries.GetSellerById.SellerDto>>().ReverseMap();// ToDo
                // config.CreateMap<CreateSellerCommand, ReimbursementPoC.Administration.Application.Product.Commands.CreateSeller.CreateSellerCommand>().ReverseMap();
                // config.CreateMap<UpdateSellerCommand, ReimbursementPoC.Administration.Application.Seller.Commands.UpdateSeller.UpdateSellerCommand>()
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<DeactivateSellerCommand, ReimbursementPoC.Administration.Application.Seller.Commands.DeactivateSeller.DeactivateSellerCommand>()
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<ReimbursementPoC.Administration.Application.Seller.Commands.UpdateSeller.UpdateSellerCommand, UpdateSellerCommand>()
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<DeleteSellerCommand, ReimbursementPoC.Administration.Application.Seller.Commands.DeleteSeller.DeleteSellerCommand>().ReverseMap();
                // config.CreateMap<ReimbursementPoC.Administration.Application.Seller.Queries.GetSellerById.SellerDto, SellerDto>()
                // .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<SellerDto, ReimbursementPoC.Administration.Application.Seller.Queries.GetSellerById.SellerDto>()
                // .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<PriceAnalytics.Administration.Services.Seller.Page, ReimbursementPoC.Administration.Application.Common.Model.Page>().ReverseMap();
            };
    }
}
