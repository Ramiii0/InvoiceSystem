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
        private readonly IRepository<InvoiceItems, Guid> _invoiceRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        public InvoiceItemAppService(IRepository<InvoiceItems, Guid> repository, IRepository<Product, Guid> productRepository )
        {
            _invoiceRepository = repository;
            _productRepository = productRepository;
        }
        public async Task<InvoiceItemsDto> CreateAsync(CreateInvoiceItemsDto input)
        {
            var query = await _productRepository.WithDetailsAsync(a => a.ProductPricing, a => a.ProductDiscount);
            var product = query.FirstOrDefault(x => x.Id == input.ProductId);
            if (product == null)
            {
                throw new Exception("There is no product with this Id");
            }
            var mappedObject = ObjectMapper.Map<Product, ProductsDto>(product);

            
            var invoiceItem = ObjectMapper.Map<CreateInvoiceItemsDto, InvoiceItems>(input);
            invoiceItem.PriceId = mappedObject.ProductPricingId;
            invoiceItem.Price= mappedObject.ProductPricingPrice;
            invoiceItem.DiscountId = mappedObject.ProductDiscountId;
            invoiceItem.DiscountValue = mappedObject.ProductDiscountDisount;
             invoiceItem.TotalPrice= mappedObject.ProductPricingPrice * input.Quantity;
            invoiceItem.TotalNet = invoiceItem.TotalPrice - (invoiceItem.TotalPrice * mappedObject.ProductDiscountDisount / 100);
            var insert = await _invoiceRepository.InsertAsync(invoiceItem);
            return ObjectMapper.Map<InvoiceItems, InvoiceItemsDto>(insert);

        }

        public async Task DeleteAsync(Guid id)
        {
           var invoiceitem = await _invoiceRepository.GetAsync(id);
            if (invoiceitem == null)
            { 
                throw new Exception("No invoiceItem with this id"); 
            }
           await _invoiceRepository.DeleteAsync(invoiceitem);
        }

        public async Task<InvoiceItemsDto> GetAsync(Guid id)
        {
            var invoiceitem = await _invoiceRepository.GetAsync(id);
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
            var query = await _invoiceRepository.GetQueryableAsync();
            query = query.OrderBy(input.Sorting);
                    

            var invoices = await query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();
            var totalCount = await _invoiceRepository.CountAsync();
            return new PagedResultDto<InvoiceItemsDto>(totalCount, ObjectMapper.Map<List<InvoiceItems>, List<InvoiceItemsDto>>(invoices));

        }

        public async Task<InvoiceItemsDto> UpdateAsync(Guid id, UpdateInvoiceItemsDto input)
        {
            var invoiceitem = await _invoiceRepository.GetAsync(id);
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
            var updated= await _invoiceRepository.UpdateAsync(invoiceitem,autoSave: true);
            return ObjectMapper.Map<InvoiceItems, InvoiceItemsDto>(updated);
        }
        
    }
}

