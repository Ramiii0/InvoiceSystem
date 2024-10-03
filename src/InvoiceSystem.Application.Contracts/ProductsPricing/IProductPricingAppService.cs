
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InvoiceSystem.ProductsPricing
{
    public interface IProductPricingAppService : IApplicationService
    {
        Task<ProductPricingDto> GetAsync(Guid id);

        Task<PagedResultDto<ProductPricingDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        Task<ProductPricingDto> CreateAsync(CreateProductPricingDto input);

        Task<ProductPricingDto> UpdateAsync(Guid id, UpdateProductPricingDto input);

        Task DeleteAsync(Guid id);
    }
}
