using InvoiceSystem.Products;
using InvoiceSystem.ProductsDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.ProductsPricing
{
    public class ProductPricingAppService : ApplicationService, IProductPricingAppService
    {
        private readonly IRepository<ProductPricing,Guid> _productPricingRepository;
        public ProductPricingAppService(IRepository<ProductPricing, Guid>  repository)
        {
            _productPricingRepository = repository;
        }
        public async Task<ProductPricingDto> CreateAsync(CreateProductPricingDto input)
        {
            var pricing = ObjectMapper.Map<CreateProductPricingDto,ProductPricing>(input);
            var insert = await _productPricingRepository.InsertAsync(pricing);
            return ObjectMapper.Map<ProductPricing, ProductPricingDto>(insert);
        }

        public async Task DeleteAsync(Guid id)
        {
            var pricing = await _productPricingRepository.FindAsync(id);
            if (pricing == null)
            {
                throw new Exception("not found");
            }
           await _productPricingRepository.DeleteAsync(pricing);
        }

        public async Task<ProductPricingDto> GetAsync(Guid id)
        {
            var pricing =  _productPricingRepository.WithDetails(x => x.Product).FirstOrDefault(x => x.Id == id);
            if (pricing == null)
            {
                throw new Exception("not found");
            }
            return ObjectMapper.Map<ProductPricing, ProductPricingDto>(pricing);
        }

        public async Task<PagedResultDto<ProductPricingDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            if (input.Sorting == null)
            {
                input.Sorting = nameof(ProductDiscount.CreationTime);
            }
            var query = await _productPricingRepository.WithDetailsAsync(a => a.Product);
            var productPricings = query.OrderBy(input.Sorting);

            var pricings = await productPricings
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();
            var totalCount = await _productPricingRepository.CountAsync();
            return new PagedResultDto<ProductPricingDto>(totalCount, ObjectMapper.Map<List<ProductPricing>, List<ProductPricingDto>>(pricings));
        }

        public async Task<ProductPricingDto> UpdateAsync(Guid id, UpdateProductPricingDto input)
        {
            var pricing = await _productPricingRepository.GetAsync(id);

            if (pricing == null)
            {
                throw new Exception("not found");
            }
            if (input.Price != 0)
            {

               pricing.Price = input.Price;
            }
            
            var insert = await _productPricingRepository.UpdateAsync(pricing, autoSave: true);
            return ObjectMapper.Map<ProductPricing, ProductPricingDto>(insert);
        }
    }
}
