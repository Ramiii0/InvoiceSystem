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

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public  async Task<ProductsDto> GetAsync(Guid id)
        {
            var query = await _productRepository.WithDetailsAsync(a => a.ProductPricing,a=>a.ProductDiscount);
         var   product = query.FirstOrDefault(x=>x.Id == id);
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
            var query = await _productRepository.GetQueryableAsync();
            query = query.OrderBy(input.Sorting)
                     .Where(x => x.ProductPricing != null); // Ensure related items are included

            var invoices = await query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();
            var totalCount = input.Filter == null
    ? await _productRepository.CountAsync()
    : await _productRepository.CountAsync(
        p => p.Name == input.Filter);
            return new PagedResultDto<ProductsDto>(totalCount, ObjectMapper.Map<List<Product>, List<ProductsDto>>(invoices));


        }

        public Task UpdateAsync(Guid id, UpdateProductDto input)
        {
            throw new NotImplementedException();
        }
    }
}
