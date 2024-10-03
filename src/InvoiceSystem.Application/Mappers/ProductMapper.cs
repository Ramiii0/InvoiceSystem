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
            CreateMap<Product,ProductsDto>();
            CreateMap<ProductsDto, Product>();
            CreateMap<CreateProductDto,Product>();
            CreateMap<UpdateProductDto, Product>();


        }

    }
}
