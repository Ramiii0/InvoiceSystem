using InvoiceSystem.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InvoiceSystem.InvoiceItem
{
    public interface IInvoiceItemsAppService : IApplicationService
    {
        Task<InvoiceItemsDto> GetAsync(Guid id);

        Task<PagedResultDto<InvoiceItemsDto>> GetListAsync(GetInvoiceItemListDto input);

        Task<InvoiceItemsDto> CreateAsync(CreateInvoiceItemsDto input);

        Task<InvoiceItemsDto> UpdateAsync(Guid id, UpdateInvoiceItemsDto input);

        Task DeleteAsync(Guid id);
    }
}
