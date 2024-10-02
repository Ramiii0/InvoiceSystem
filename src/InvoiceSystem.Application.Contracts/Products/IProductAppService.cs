using InvoiceSystem.InvoiceItem;
using InvoiceSystem.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InvoiceSystem.Products
{
    public interface IProductAppService : IApplicationService
    {
        Task<ProductsDto> GetAsync(Guid id);

        Task<PagedResultDto<ProductsDto>> GetListAsync(GetProductListDto input);

        Task<ProductsDto> CreateAsync(CreateProductDto input);

        Task UpdateAsync(Guid id, UpdateProductDto input);

        Task DeleteAsync(Guid id);

    }
}
