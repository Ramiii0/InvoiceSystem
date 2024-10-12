using AutoMapper;
using InvoiceSystem.ProductPricings;

using InvoiceSystem.ProductsPricings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Mappers
{
    public class ProductPricingMapper : Profile
    {
        public ProductPricingMapper()
        {
            CreateMap<ProductPricing, ProductPricingDto>();
            CreateMap<ProductPricingDto, ProductPricing>();
            CreateMap<CreateProductPricingDto, ProductPricing>();
            CreateMap<UpdateProductPricingDto, ProductPricing>();

        }
    }
}
