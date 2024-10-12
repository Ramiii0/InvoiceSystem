using AutoMapper;
using InvoiceSystem.Products;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper() 
        {
            CreateMap<Product, ProductsDto>().ForMember(dest => dest.PriceDetails,
               opt => opt.MapFrom(src => src.ProductPricing
                                             .OrderByDescending(pp => pp.CreationTime)
                                             .Select(pp => new ProductPriceDto
                                             {
                                                 Id = pp.Id,
                                                 Price = pp.Price,
                                                 PriceDuration = pp.PriceDuration
                                                 
                                             })
                                             .FirstOrDefault()))
                .ForMember(dest => dest.DiscountDetails,
               opt => opt.MapFrom(src => src.ProductDiscount
                                             .OrderByDescending(pp => pp.CreationTime)
                                             .Select(pp => new DiscountDetails
                                             {
                                                 Id = pp.Id,
                                                 Discount = pp.Disount
                                             })
                                             .FirstOrDefault()));
               /* .ForMember(d => d.ProductDiscount, op => op.MapFrom(src => src.ProductDiscount));*/
            CreateMap<ProductsDto, Product>();
            CreateMap<CreateProductDto,Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<ProductsDto, UpdateProductDto>();


        }

    }
}
