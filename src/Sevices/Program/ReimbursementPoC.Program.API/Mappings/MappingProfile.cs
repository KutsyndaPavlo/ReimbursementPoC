using AutoMapper;

namespace ReimbursementPoC.Program.API.Mappings
{
    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
               // config.CreateMap<GetProductsQuery, ReimbursementPoC.Program.Application.Product.Queries.GetProducts.GetProductsQuery>().ReverseMap();
               // config.CreateMap<GetProductByIdQuery, ReimbursementPoC.Program.Application.Product.Queries.GetProductById.GetProductByIdQuery>().ReverseMap();
               // config.CreateMap<PriceAnalytics.Administration.Services.Product.Page, ReimbursementPoC.Program.Application.Common.Model.Page>().ReverseMap();
               // config.CreateMap<ProductPaginatedList, PaginatedList<ReimbursementPoC.Program.Application.Product.Queries.GetProductById.ProductDto>>().ReverseMap();// ToDo
               // config.CreateMap<CreateProductCommand, ReimbursementPoC.Program.Application.Product.Commands.CreateProduct.CreateProductCommand>().ReverseMap();
               // config.CreateMap<UpdateProductCommand, ReimbursementPoC.Program.Application.Product.Commands.UpdateProduct.UpdateProductCommand>()
               // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
               // config.CreateMap<DeactivateProductCommand, ReimbursementPoC.Program.Application.Product.Commands.DeactivateProduct.DeactivateProductCommand>()
               // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
               // config.CreateMap<ReimbursementPoC.Program.Application.Product.Commands.UpdateProduct.UpdateProductCommand, UpdateProductCommand>()
               // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
               // config.CreateMap<DeleteProductCommand, ReimbursementPoC.Program.Application.Product.Commands.DeleteProduct.DeleteProductCommand>().ReverseMap();
               // config.CreateMap<ReimbursementPoC.Program.Application.Product.Queries.GetProductById.ProductDto, ProductDto>()
               // .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
               // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
               // config.CreateMap<ProductDto, ReimbursementPoC.Program.Application.Product.Queries.GetProductById.ProductDto>()
               // .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
               // .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));

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
