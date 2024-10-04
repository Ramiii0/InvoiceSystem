using InvoiceSystem.Invoices;
using InvoiceSystem.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace InvoiceSystem.InvoiceItem
{
    public class InvoiceItemAppService : ApplicationService, IInvoiceItemsAppService
    {
        private readonly IRepository<InvoiceItems, Guid> _invoiceItemRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<Invoice, Guid> _invoice;
        public InvoiceItemAppService(IRepository<Invoice, Guid> invoice, IRepository<InvoiceItems, Guid> repository, IRepository<Product, Guid> productRepository )
        {
            _invoiceItemRepository = repository;
            _productRepository = productRepository;
            _invoice = invoice;
        }
        public async Task<InvoiceItemsDto> CreateAsync(CreateInvoiceItemsDto input)
        {
            var product =  _productRepository.WithDetails(a => a.ProductPricing, a => a.ProductDiscount).FirstOrDefault(x => x.Id == input.ProductId);
           
            if (product == null || product.ProductPricing == null)
            {
                throw new Exception("Product does not have price");
            }
            
            var mappedObject = ObjectMapper.Map<Product, ProductsDto>(product);

            
            var invoiceItem = ObjectMapper.Map<CreateInvoiceItemsDto, InvoiceItems>(input);
            decimal discount = (mappedObject.ProductPricingPrice *  mappedObject.ProductDiscountDisount / 100) * input.Quantity;
            var total = invoiceItem.TotalPrice = mappedObject.ProductPricingPrice * input.Quantity;
            invoiceItem.PriceId = mappedObject.ProductPricingId;
            invoiceItem.Price= mappedObject.ProductPricingPrice;
            invoiceItem.DiscountId = mappedObject.ProductDiscountId;
            invoiceItem.DiscountValue = mappedObject.ProductDiscountDisount;
            
            invoiceItem.TotalNet = total - discount;
           await AddTotalPriceToInvoice(input.InvoiceId, total, discount);
            var insert = await _invoiceItemRepository.InsertAsync(invoiceItem);
            return ObjectMapper.Map<InvoiceItems, InvoiceItemsDto>(insert);

        }

        public async Task DeleteAsync(Guid id)
        {
           var invoiceitem = await _invoiceItemRepository.GetAsync(id);
            if (invoiceitem == null)
            { 
                throw new Exception("No invoiceItem with this id"); 
            }
            await RemoveOtemFromInvoice(invoiceitem.InvoiceId, invoiceitem.TotalPrice, invoiceitem.TotalNet);
            await _invoiceItemRepository.DeleteAsync(invoiceitem);
           
        }

        public async Task<InvoiceItemsDto> GetAsync(Guid id)
        {
            var invoiceitem = await _invoiceItemRepository.GetAsync(id);
            if (invoiceitem == null)
            {
                throw new Exception("No invoiceItem with this id");
            }
            return ObjectMapper.Map<InvoiceItems, InvoiceItemsDto>(invoiceitem);
        }

        public async Task<PagedResultDto<InvoiceItemsDto>> GetListAsync(GetInvoiceItemListDto input)
        {

            if (input.Sorting == null)
            {
                input.Sorting = nameof(InvoiceItems.CreationTime);
            }
            var query = await _invoiceItemRepository.GetQueryableAsync();
            query = query.OrderBy(input.Sorting);
                    

            var invoices = await query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();
            var totalCount = await _invoiceItemRepository.CountAsync();
            return new PagedResultDto<InvoiceItemsDto>(totalCount, ObjectMapper.Map<List<InvoiceItems>, List<InvoiceItemsDto>>(invoices));

        }

        public async Task<InvoiceItemsDto> UpdateAsync(Guid id, UpdateInvoiceItemsDto input)
        {
            var invoiceitem = await _invoiceItemRepository.GetAsync(id);
            if (invoiceitem == null)
            {
                throw new Exception("No invoiceItem with this id");
            }
            if(input.InvoiceId != default(Guid))
            {
                invoiceitem.InvoiceId = input.InvoiceId;


            }
            if (input.ProductId != default(Guid) || input.Quantity != 0)
            {

                Guid p_id=invoiceitem.ProductId;
                if(input.ProductId != default(Guid))
                {
                    p_id= input.ProductId;
                    invoiceitem.ProductId = input.ProductId;
                }

                var query = await _productRepository.WithDetailsAsync(a => a.ProductPricing, a => a.ProductDiscount);
                var product = query.FirstOrDefault(x => x.Id == p_id );
                if (product == null)
                {
                    throw new Exception("There is no product with this Id");
                }
                var mappedObject = ObjectMapper.Map<Product, ProductsDto>(product);



                invoiceitem.PriceId = mappedObject.ProductPricingId;
                invoiceitem.Price = mappedObject.ProductPricingPrice;
                invoiceitem.DiscountId = mappedObject.ProductDiscountId;
                invoiceitem.DiscountValue = mappedObject.ProductDiscountDisount;
                invoiceitem.TotalPrice = mappedObject.ProductPricingPrice * input.Quantity;
                invoiceitem.TotalNet = invoiceitem.TotalPrice - (invoiceitem.TotalPrice * mappedObject.ProductDiscountDisount / 100);


            }
            var updated= await _invoiceItemRepository.UpdateAsync(invoiceitem,autoSave: true);
            return ObjectMapper.Map<InvoiceItems, InvoiceItemsDto>(updated);
        }
        private async Task  AddTotalPriceToInvoice (Guid id,decimal price,decimal discount)
        {
            var invoice = await _invoice.GetAsync(id);
            if (invoice == null)
            {
                throw new Exception("no invoice found");
            }
            invoice.InvoiceAmount +=  price;
            invoice.TotalDiscount += discount;
            invoice.NetAmount =invoice.InvoiceAmount - invoice.TotalDiscount;
            await _invoice.UpdateAsync(invoice,autoSave: true);
            



        }
        private async Task RemoveOtemFromInvoice(Guid id, decimal totalprice, decimal net)
        {
            var invoice = await _invoice.GetAsync(id);
            if (invoice == null)
            {
                throw new Exception("no invoice found");
            }
            decimal discountValue= totalprice-net;
            invoice.InvoiceAmount -=   totalprice;
            invoice.TotalDiscount -=   discountValue;
            invoice.NetAmount -=  net ;
            await _invoice.UpdateAsync(invoice, autoSave: true);
            


        }
        
    }
}

