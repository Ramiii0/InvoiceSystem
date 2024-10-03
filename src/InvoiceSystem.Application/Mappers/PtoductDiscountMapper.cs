using AutoMapper;
using InvoiceSystem.Products;
using InvoiceSystem.ProductsDiscount;
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
