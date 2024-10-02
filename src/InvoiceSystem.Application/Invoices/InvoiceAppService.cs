using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using Microsoft.EntityFrameworkCore;
using InvoiceSystem.Products;

namespace InvoiceSystem.Invoices
{
    public class InvoiceAppService : ApplicationService, IInvoiceAppService
    {
        private readonly IRepository<Invoice,Guid> _invoiceRepository;
        public InvoiceAppService(IRepository<Invoice, Guid>  repository)
        {
            _invoiceRepository = repository;
        }
        public async Task<InvoiceDto> CreateAsync(CreateInvoiceDto input)
        {
            var invoice = ObjectMapper.Map<CreateInvoiceDto, Invoice>(input);
            var insert = await _invoiceRepository.InsertAsync(invoice);
            return  ObjectMapper.Map< Invoice, InvoiceDto>(insert);

        }

        public async Task DeleteAsync(Guid id)
        {
            var invoice = await _invoiceRepository.FindAsync(id);
            if (invoice == null)
            {
                throw new Exception("Invoice was not found");
            }
            await _invoiceRepository.DeleteAsync(invoice);
        }

        public async Task<InvoiceDto> GetAsync(Guid id)
        {
            var invoice = await _invoiceRepository.GetAsync(id);
            if(invoice == null)
            {
                throw new Exception("Invoice was not found");
            }
            return ObjectMapper.Map<Invoice, InvoiceDto>(invoice);
        }

        public async Task<PagedResultDto<InvoiceDto>> GetListAsync(GetInvoiceListDto input)
        {
            if (input.Sorting == null)
            {
                input.Sorting = nameof(Invoice.CreationTime);
            }
            var query = await _invoiceRepository.GetQueryableAsync();
            query = query.OrderBy(input.Sorting)
                     .Where(x => x.InvoiceItems != null); // Ensure related items are included

            var invoices = await query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();
            var totalCount = input.Filter == null
    ? await _invoiceRepository.CountAsync()
    : await _invoiceRepository.CountAsync(
        p => p.InvoiceNo == input.Filter);
            return new PagedResultDto<InvoiceDto>(totalCount, ObjectMapper.Map<List<Invoice>, List<InvoiceDto>>(invoices));
        }

        public Task UpdateAsync(Guid id, UpdateInvoiceDto input)
        {
            throw new NotImplementedException();
        }
    }
}
