﻿using API.DTOs;
using AutoMapper;
using Core.Entities;

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
            
        }
    }
}
