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
using InvoiceSystem.ProductDiscounts;
using InvoiceSystem.ProductsDiscounts;

namespace InvoiceSystem.ProductDiscounts
{
    public class ProductDiscountAppService : ApplicationService, IProductDiscountAppService
    {
        private readonly IProductDiscountRepository _productService;
      
        public ProductDiscountAppService(IProductDiscountRepository repository)
        {
            _productService = repository;
           
        }
        public async Task<ProductDiscountDto> CreateAsync(CreateProductDiscountDto input)
        {
           var discount = ObjectMapper.Map<CreateProductDiscountDto,ProductDiscount>(input);
            var insert = await _productService.InsertAsync(discount);
           

            return ObjectMapper.Map<ProductDiscount,ProductDiscountDto>(insert);
        }

        public async Task DeleteAsync(Guid id)
        {
            var discount = await _productService.GetAsync(id);
            if (discount == null)
            {
                throw new Exception("not found");
            }
            await _productService.DeleteAsync(discount);
        }

        public async Task<ProductDiscountDto> GetAsync(Guid id)
        {
            var discount =  _productService.WithDetails(x => x.Product).FirstOrDefault(x=>x.Id == id);
           // var discount = query.FirstOrDefault(x => x.Id == id);
            

            if (discount == null)
            {
                throw new Exception("not found");
            }
            return ObjectMapper.Map<ProductDiscount, ProductDiscountDto>(discount);
        }

        public  async Task<PagedResultDto<ProductDiscountDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            if(input.Sorting == null)
            {
                input.Sorting = nameof(ProductDiscount.CreationTime);
            }
            var query = await _productService.WithDetailsAsync(a => a.Product);
            var product = query.OrderBy(input.Sorting);

            var discounts = await product
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();
            var totalCount = await _productService.CountAsync();
            return new PagedResultDto<ProductDiscountDto>(totalCount, ObjectMapper.Map<List<ProductDiscount>, List<ProductDiscountDto>>(discounts));
        }

        public async Task<ProductDiscountDto> UpdateAsync(Guid id, UpdateProductDiscountDto input)
        {
           
            var discount = await _productService.GetAsync(id);

            if (discount == null)
            {
                throw new Exception("not found");
            }
            if(input.ProductId != default(Guid))
            {

           discount.ProductId = input.ProductId; 
            }
           
            if(input.StartDate != default(DateTime))
            {
                discount.StartDate = input.StartDate;
            }
            if (input.EndDate != default(DateTime))
            {
                discount.EndDate = input.EndDate;
            }
            var insert = await _productService.UpdateAsync(discount,autoSave: true);
            return ObjectMapper.Map<ProductDiscount, ProductDiscountDto>(insert);

        }
       
    }
}
