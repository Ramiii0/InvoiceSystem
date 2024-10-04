using InvoiceSystem.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.ObjectMapping;
using AutoMapper.Internal.Mappers;

namespace InvoiceSystem.Products
{
    public class  ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;
        public ProductAppService(IRepository<Product, Guid> repository)
        {
            _productRepository = repository;
            
        }
        public async Task<ProductsDto> CreateAsync(CreateProductDto input)
        {
            var product = ObjectMapper.Map<CreateProductDto, Product>(input);
           var insert= await _productRepository.InsertAsync(product);
            return ObjectMapper.Map<Product,ProductsDto>(insert);
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product == null)
            {
                throw new Exception("Product Not Found");
            }
            await _productRepository.DeleteAsync(product);
        }

        public  async Task<ProductsDto> GetAsync(Guid id)
        {
            var product =  _productRepository.WithDetails(a => a.ProductPricing,a=>a.ProductDiscount).FirstOrDefault(x => x.Id == id);
            //var   product = query.
            if (product == null)
            {
                throw new Exception("Product Not Found");
            }

           
            return ObjectMapper.Map<Product, ProductsDto>(product);
            
        }

        public async Task<PagedResultDto<ProductsDto>> GetListAsync(GetProductListDto input)
        {
            if (input.Sorting == null)
            {
                input.Sorting = input.Sorting = nameof(Product.CreationTime);
            }
          
            var query = await _productRepository.WithDetailsAsync(a => a.ProductPricing, a => a.ProductDiscount);
            var product = query.WhereIf(!input.Filter.IsNullOrWhiteSpace(), p => p.Name.Contains(input.Filter)).OrderBy(input.Sorting);

            var invoices = await product
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();
            var totalCount = input.Filter == null
    ? await _productRepository.CountAsync()
    : await _productRepository.CountAsync(
        p => p.Name == input.Filter);
            return new PagedResultDto<ProductsDto>(totalCount, ObjectMapper.Map<List<Product>, List<ProductsDto>>(invoices));


        }

        public async Task<ProductsDto> UpdateAsync(Guid id, UpdateProductDto input)
        {
            var product = await _productRepository.GetAsync(id);
            if (product == null)
            {
                throw new Exception("No Product with this id");
            }
            if (input.Name != null)
            {
                product.Name = input.Name;

            }
            if(input.Code != 0)
            {
                product.Code = input.Code; 
            }
            if (input.PartNo != 0)
            {
                product.PartNo = input.PartNo;
            }
            var insert = await _productRepository.UpdateAsync(product,autoSave : true);
            return  ObjectMapper.Map<Product,ProductsDto>(insert);

        }
    }
}
