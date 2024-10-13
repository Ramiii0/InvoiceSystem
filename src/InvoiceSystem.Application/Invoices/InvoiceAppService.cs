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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Authorization;
using InvoiceSystem.Permissions;
using System.Web.Http;
using static InvoiceSystem.Permissions.InvoiceSystemPermissions;
using InvoiceSystem.InvoiceItems;
using Volo.Abp;
using Volo.Abp.Data;

namespace InvoiceSystem.Invoices
{
    public class InvoiceAppService : ApplicationService, IInvoiceAppService
    {
        
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepo;
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        private readonly IInvoiceItemsAppService _invoiceItemAppService;
        private readonly IDataFilter _dataFilter;
        public InvoiceAppService( IInvoiceRepository invoiceAppService, IProductRepository productRepo, IInvoiceItemRepository invoiceItemRepository, IInvoiceItemsAppService invoiceItemAppService,  IDataFilter dataFilter)
        {
            _invoiceRepository = invoiceAppService;
            _productRepo = productRepo;
            _invoiceItemRepository = invoiceItemRepository;
            _invoiceItemAppService = invoiceItemAppService;
            _dataFilter = dataFilter;
        }
        //[Authorize(InvoiceSystemPermissions.Invoices.Create)]
        public async Task<InvoiceDto> CreateAsync(CreateInvoiceDto input)
        {
            var customerName = new Invoice() { CustomerName = input.CustomerName };

            var insert = await  _invoiceRepository.InsertAsync(customerName);
            await CurrentUnitOfWork.SaveChangesAsync();
           

            await _invoiceItemAppService.CreateAsync(input.invoiceItems,insert.Id);
            
            return ObjectMapper.Map<Invoice, InvoiceDto>(insert);

        }
        // [Authorize(InvoiceSystemPermissions.Invoices.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var invoice = await _invoiceRepository.FindAsync(id);
            if (invoice == null)
            {
                throw new Exception("Invoice was not found");
            }
            await _invoiceRepository.DeleteAsync(invoice);
        }

        public Task<InvoiceDto> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<InvoiceDto> GetAsync(Guid id)
        {
            Invoice invoice = await _invoiceRepository.GetInvoice(id);
            // var invoice = await _invoiceRepository.GetAsync(id,includeDetails: true);
            if (invoice == null)
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
            List<Invoice> invoices = await _invoiceRepository.GetAll(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);
            var totalCount = input.Filter == null
        ? await _invoiceRepository.CountAsync()
        : await _invoiceRepository.CountAsync(p => p.InvoiceNo == input.Filter);
            return new PagedResultDto<InvoiceDto>(totalCount, ObjectMapper.Map<List<Invoice>, List<InvoiceDto>>(invoices));
        }
        // [Authorize(InvoiceSystemPermissions.Invoices.Edit)]
        public async Task<InvoiceDto> UpdateAsync(Guid id, UpdateInvoiceDto input)
        {
            var invoice = await _invoiceRepository.GetAsync(id);
            if (invoice == null)
            {
                throw new Exception("Invoice was not found");
            }
            var maped = ObjectMapper.Map<UpdateInvoiceDto, Invoice>(input, invoice);
            var updated = await _invoiceRepository.UpdateAsync(maped, autoSave: true);
            return ObjectMapper.Map<Invoice, InvoiceDto>(updated);
        }
        public async Task RestoreDelete(Guid id)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var invoice = await _invoiceRepository.GetAsync(id);
                if (invoice != null && invoice.IsDeleted)
                {
                    invoice.IsDeleted = false;
                    await _invoiceRepository.UpdateAsync(invoice, autoSave: true);
                    return;
                }

                throw new Exception();


            }
        }

    } 
    
}
