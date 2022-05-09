using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        private readonly IConfiguration config;

        public MappingProfiles()
        {
        
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                   .ForMember(d => d.Pictures, o => o.MapFrom(s => s.Pictures.Select(x=>x.ImageUrl)))

                ;

            CreateMap<Review, ReviewDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name)
                );
                
            CreateMap<ProductType, ProductTypeDto>().ReverseMap();

            CreateMap<ProductBrand, ProductBrandDto>().ReverseMap();

            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom( s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom( s => s.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName));
        }
    }
}
