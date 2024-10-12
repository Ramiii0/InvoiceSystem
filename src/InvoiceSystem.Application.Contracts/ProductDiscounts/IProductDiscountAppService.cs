
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InvoiceSystem.ProductsDiscounts
{
    public interface IProductDiscountAppService : IApplicationService
    {
        Task<ProductDiscountDto> GetAsync(Guid id);

        Task<PagedResultDto<ProductDiscountDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        Task<ProductDiscountDto> CreateAsync(CreateProductDiscountDto input);

        Task<ProductDiscountDto> UpdateAsync(Guid id, UpdateProductDiscountDto input);

        Task DeleteAsync(Guid id);
    }
}
