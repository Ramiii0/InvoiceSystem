using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InvoiceSystem.Invoices
{
    public interface IInvoiceAppService : IApplicationService
    {
        Task<InvoiceDto> GetAsync(Guid id);

        Task<PagedResultDto<InvoiceDto>> GetListAsync(GetInvoiceListDto input);

        Task<InvoiceDto> CreateAsync(CreateInvoiceDto input);

        Task<InvoiceDto> UpdateAsync(Guid id, UpdateInvoiceDto input);

        Task DeleteAsync(Guid id);

    }
    
}
