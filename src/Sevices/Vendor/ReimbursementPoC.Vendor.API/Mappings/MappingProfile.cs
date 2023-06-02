using AutoMapper;
using ReimbursementPoC.Vendor.API.Models;

namespace ReimbursementPoC.Vendor.API.Mappings
{
    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
                //config.CreateMap<GetVendorsQuery, ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendors.GetVendorsQuery>().ReverseMap();
                //config.CreateMap<GetVendorByIdQuery, ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById.GetVendorByIdQuery>().ReverseMap();
                //config.CreateMap<PriceAnalytics.Administration.Services.Vendor.Page, ReimbursementPoC.Vendor.Application.Common.Model.Page>().ReverseMap();
                //config.CreateMap<VendorPaginatedList, PaginatedList<ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById.VendorSubmissionDto>>().ReverseMap();// ToDo
                config.CreateMap<CreateVendorRequest, ReimbursementPoC.Vendor.Application.Vendor.Commands.CreateVendor.CreateVendorSubmissionCommand>().ReverseMap();
      
                // config.CreateMap<UpdateServiceRequest, UpdateServiceCommand>().ReverseMap();
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                //config.CreateMap<DeactivateVendorCommand, ReimbursementPoC.Vendor.Application.Vendor.Commands.DeactivateVendor.DeactivateVendorCommand>()
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                //config.CreateMap<ReimbursementPoC.Vendor.Application.Vendor.Commands.UpdateVendor.UpdateVendorCommand, UpdateVendorCommand>()
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                //config.CreateMap<DeleteVendorCommand, ReimbursementPoC.Vendor.Application.Vendor.Commands.DeleteVendor.DeleteVendorCommand>().ReverseMap();
                //config.CreateMap<ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById.VendorSubmissionDto, VendorSubmissionDto>()
                //.ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<VendorSubmissionDto, ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById.VendorSubmissionDto>();
                //.ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));

                // config.CreateMap<CreateProposalCommand, ReimbursementPoC.Vendor.Application.Proposal.Commands.CreateProposal.CreateProposalCommand>().ReverseMap();
                // config.CreateMap<ProposalDto, ReimbursementPoC.Vendor.Application.Proposal.Queries.GetProposalById.ProposalDto>()
                // .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<ReimbursementPoC.Vendor.Application.Proposal.Queries.GetProposalById.ProposalDto, ProposalDto>()
                //.ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<GetProposalsQuery, ReimbursementPoC.Vendor.Application.Proposal.Queries.GetProposals.GetProposalsQuery>().ReverseMap();
                // config.CreateMap<GetProposalByIdQuery, ReimbursementPoC.Vendor.Application.Proposal.Queries.GetProposalById.GetProposalByIdQuery>().ReverseMap();
                // config.CreateMap<PriceAnalytics.Administration.Services.Proposal.Page, ReimbursementPoC.Vendor.Application.Common.Model.Page>();
                // config.CreateMap<DeleteProposalCommand, ReimbursementPoC.Vendor.Application.Proposal.Commands.DeleteProposal.DeleteProposalCommand>().ReverseMap();
                // config.CreateMap<ProposalPaginatedList, PaginatedList<ReimbursementPoC.Vendor.Application.Proposal.Queries.GetProposalById.ProposalDto>>().ReverseMap();// ToDo
                // config.CreateMap<PriceAnalytics.Administration.Services.Proposal.Page, ReimbursementPoC.Vendor.Application.Common.Model.Page>().ReverseMap();

                // config.CreateMap<GetSellersQuery, ReimbursementPoC.Vendor.Application.Seller.Queries.GetSellers.GetSellersQuery>().ReverseMap();
                // config.CreateMap<GetSellerByIdQuery, ReimbursementPoC.Vendor.Application.Seller.Queries.GetSellerById.GetSellerByIdQuery>().ReverseMap();
                // config.CreateMap<PriceAnalytics.Administration.Services.Seller.Page, ReimbursementPoC.Vendor.Application.Common.Model.Page>();
                // config.CreateMap<SellerPaginatedList, PaginatedList<ReimbursementPoC.Vendor.Application.Seller.Queries.GetSellerById.SellerDto>>().ReverseMap();// ToDo
                // config.CreateMap<CreateSellerCommand, ReimbursementPoC.Vendor.Application.Product.Commands.CreateSeller.CreateSellerCommand>().ReverseMap();
                // config.CreateMap<UpdateSellerCommand, ReimbursementPoC.Vendor.Application.Seller.Commands.UpdateSeller.UpdateSellerCommand>()
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<DeactivateSellerCommand, ReimbursementPoC.Vendor.Application.Seller.Commands.DeactivateSeller.DeactivateSellerCommand>()
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<ReimbursementPoC.Vendor.Application.Seller.Commands.UpdateSeller.UpdateSellerCommand, UpdateSellerCommand>()
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<DeleteSellerCommand, ReimbursementPoC.Vendor.Application.Seller.Commands.DeleteSeller.DeleteSellerCommand>().ReverseMap();
                // config.CreateMap<ReimbursementPoC.Vendor.Application.Seller.Queries.GetSellerById.SellerDto, SellerDto>()
                // .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<SellerDto, ReimbursementPoC.Vendor.Application.Seller.Queries.GetSellerById.SellerDto>()
                // .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<PriceAnalytics.Administration.Services.Seller.Page, ReimbursementPoC.Vendor.Application.Common.Model.Page>().ReverseMap();
            };
    }
}
