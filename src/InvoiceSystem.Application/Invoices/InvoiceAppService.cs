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

namespace InvoiceSystem.Invoices
{
    public class InvoiceAppService : ApplicationService, IInvoiceAppService
    {
        
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepo;
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        private readonly IInvoiceItemsAppService _invoiceItemAppService;
        public InvoiceAppService( IInvoiceRepository invoiceAppService, IProductRepository productRepo, IInvoiceItemRepository invoiceItemRepository, IInvoiceItemsAppService invoiceItemAppService)
        {
            _invoiceRepository = invoiceAppService;
            _productRepo = productRepo;
            _invoiceItemRepository = invoiceItemRepository;
            _invoiceItemAppService = invoiceItemAppService;
        }
        //[Authorize(InvoiceSystemPermissions.Invoices.Create)]
        public async Task<InvoiceDto> CreateAsync(CreateInvoiceDto input)
        {
            var customerName = new Invoice() { CustomerName = input.CustomerName };

            var insert = await  _invoiceRepository.InsertAsync(customerName);
            await CurrentUnitOfWork.SaveChangesAsync();
           

            await _invoiceItemAppService.CreateAsync(input.invoiceItems,insert.Id);
            /*foreach (var item in input.invoiceItems)
            {
                CreateInvoiceItemsDto invoiceItems = item;
                invoiceItems.InvoiceId= insert.Id;
                //await AddItemsToInvoice(invoiceItems);
             await   _invoiceItemAppService.CreateAsync(invoiceItems);

            }*/
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
       /* private async Task AddItemsToInvoice( CreateInvoiceItemsDto createInvoiceItemsDto)
        {
            var product = await _productRepo.GetProduct(createInvoiceItemsDto.ProductId);
            var mappedProduct = ObjectMapper.Map<Product, ProductsDto>(product);
            var invoiceItem = ObjectMapper.Map<CreateInvoiceItemsDto, InvoiceItem>(createInvoiceItemsDto);
            decimal totaldiscount = mappedProduct.DiscountDetails != null ?
                (mappedProduct.PriceDetails.Price * mappedProduct.DiscountDetails.Discount / 100) * createInvoiceItemsDto.Quantity : 0;

            var totalprice = invoiceItem.TotalPrice = mappedProduct.PriceDetails.Price * createInvoiceItemsDto.Quantity;
            invoiceItem.PriceId = mappedProduct.PriceDetails.Id;
            invoiceItem.Price = mappedProduct.PriceDetails.Price;
            invoiceItem.DiscountId = mappedProduct.DiscountDetails != null ? mappedProduct.DiscountDetails.Id : default(Guid);
            invoiceItem.TotalDiscount = totaldiscount;

            decimal totalnet = invoiceItem.TotalNet = totalprice - totaldiscount;
            await AddTotalPriceToInvoice(createInvoiceItemsDto.InvoiceId, totalprice, totaldiscount, totalnet);
            var insert = await _invoiceItemRepository.InsertAsync(invoiceItem);
        }*/
       /* public async Task AddTotalPriceToInvoice(Guid id, decimal totalprice, decimal totaldiscount, decimal totalnet)
        {
            var invoice = await _invoiceRepository.GetAsync(id);
            if (invoice == null)
            {
                throw new Exception("no invoice found");
            }

            invoice.InvoiceAmount += totalprice;
            invoice.TotalDiscount += totaldiscount;
            invoice.NetAmount += totalnet;
            await _invoiceRepository.UpdateAsync(invoice, autoSave: true);




        }*/
        

    } 
    
}
