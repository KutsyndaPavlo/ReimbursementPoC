using AutoMapper;
using ReimbursementPoC.Customer.API.Models;

namespace ReimbursementPoC.Customer.API.Mappings
{
    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
                //config.CreateMap<GetCustomersQuery, ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomers.GetCustomersQuery>().ReverseMap();
                //config.CreateMap<GetCustomerByIdQuery, ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById.GetCustomerByIdQuery>().ReverseMap();
                //config.CreateMap<PriceAnalytics.Administration.Services.Customer.Page, ReimbursementPoC.Customer.Application.Common.Model.Page>().ReverseMap();
                //config.CreateMap<CustomerPaginatedList, PaginatedList<ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById.CustomerSubmissionDto>>().ReverseMap();// ToDo
                config.CreateMap<CreateCustomerSubmissionRequest, ReimbursementPoC.Customer.Application.Customer.Commands.CreateCustomer.CreateCustomerSubmissionCommand>().ReverseMap();
      
                // config.CreateMap<UpdateServiceRequest, UpdateServiceCommand>().ReverseMap();
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                //config.CreateMap<DeactivateCustomerCommand, ReimbursementPoC.Customer.Application.Customer.Commands.DeactivateCustomer.DeactivateCustomerCommand>()
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                //config.CreateMap<ReimbursementPoC.Customer.Application.Customer.Commands.UpdateCustomer.UpdateCustomerCommand, UpdateCustomerCommand>()
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                //config.CreateMap<DeleteCustomerCommand, ReimbursementPoC.Customer.Application.Customer.Commands.DeleteCustomer.DeleteCustomerCommand>().ReverseMap();
                //config.CreateMap<ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById.CustomerSubmissionDto, CustomerSubmissionDto>()
                //.ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<CustomerSubmissionDto, ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById.CustomerSubmissionDto>();
                //.ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));

                // config.CreateMap<CreateProposalCommand, ReimbursementPoC.Customer.Application.Proposal.Commands.CreateProposal.CreateProposalCommand>().ReverseMap();
                // config.CreateMap<ProposalDto, ReimbursementPoC.Customer.Application.Proposal.Queries.GetProposalById.ProposalDto>()
                // .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<ReimbursementPoC.Customer.Application.Proposal.Queries.GetProposalById.ProposalDto, ProposalDto>()
                //.ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                //.ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<GetProposalsQuery, ReimbursementPoC.Customer.Application.Proposal.Queries.GetProposals.GetProposalsQuery>().ReverseMap();
                // config.CreateMap<GetProposalByIdQuery, ReimbursementPoC.Customer.Application.Proposal.Queries.GetProposalById.GetProposalByIdQuery>().ReverseMap();
                // config.CreateMap<PriceAnalytics.Administration.Services.Proposal.Page, ReimbursementPoC.Customer.Application.Common.Model.Page>();
                // config.CreateMap<DeleteProposalCommand, ReimbursementPoC.Customer.Application.Proposal.Commands.DeleteProposal.DeleteProposalCommand>().ReverseMap();
                // config.CreateMap<ProposalPaginatedList, PaginatedList<ReimbursementPoC.Customer.Application.Proposal.Queries.GetProposalById.ProposalDto>>().ReverseMap();// ToDo
                // config.CreateMap<PriceAnalytics.Administration.Services.Proposal.Page, ReimbursementPoC.Customer.Application.Common.Model.Page>().ReverseMap();

                // config.CreateMap<GetSellersQuery, ReimbursementPoC.Customer.Application.Seller.Queries.GetSellers.GetSellersQuery>().ReverseMap();
                // config.CreateMap<GetSellerByIdQuery, ReimbursementPoC.Customer.Application.Seller.Queries.GetSellerById.GetSellerByIdQuery>().ReverseMap();
                // config.CreateMap<PriceAnalytics.Administration.Services.Seller.Page, ReimbursementPoC.Customer.Application.Common.Model.Page>();
                // config.CreateMap<SellerPaginatedList, PaginatedList<ReimbursementPoC.Customer.Application.Seller.Queries.GetSellerById.SellerDto>>().ReverseMap();// ToDo
                // config.CreateMap<CreateSellerCommand, ReimbursementPoC.Customer.Application.Product.Commands.CreateSeller.CreateSellerCommand>().ReverseMap();
                // config.CreateMap<UpdateSellerCommand, ReimbursementPoC.Customer.Application.Seller.Commands.UpdateSeller.UpdateSellerCommand>()
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<DeactivateSellerCommand, ReimbursementPoC.Customer.Application.Seller.Commands.DeactivateSeller.DeactivateSellerCommand>()
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<ReimbursementPoC.Customer.Application.Seller.Commands.UpdateSeller.UpdateSellerCommand, UpdateSellerCommand>()
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<DeleteSellerCommand, ReimbursementPoC.Customer.Application.Seller.Commands.DeleteSeller.DeleteSellerCommand>().ReverseMap();
                // config.CreateMap<ReimbursementPoC.Customer.Application.Seller.Queries.GetSellerById.SellerDto, SellerDto>()
                // .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                // config.CreateMap<SellerDto, ReimbursementPoC.Customer.Application.Seller.Queries.GetSellerById.SellerDto>()
                // .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                // config.CreateMap<PriceAnalytics.Administration.Services.Seller.Page, ReimbursementPoC.Customer.Application.Common.Model.Page>().ReverseMap();
            };
    }
}
