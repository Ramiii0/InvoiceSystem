using AutoMapper;
using InvoiceSystem.ProductDiscounts;

using InvoiceSystem.ProductsDiscounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Mappers
{
    public class PtoductDiscountMapper : Profile
    {
        public PtoductDiscountMapper()
        {
            CreateMap<ProductDiscount, ProductDiscountDto>();
            CreateMap<ProductDiscountDto, ProductDiscount>();
            CreateMap<CreateProductDiscountDto, ProductDiscount>();
            CreateMap<UpdateProductDiscountDto, ProductDiscount>();

        }
    }
}
