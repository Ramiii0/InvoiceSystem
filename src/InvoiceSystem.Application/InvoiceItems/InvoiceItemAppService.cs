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

using InvoiceSystem.InvoiceItems;
using Volo.Abp.Users;
using Volo.Abp.Data;
using Volo.Abp;
using InvoiceSystem.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceSystem.InvoiceItems
{
    public  class InvoiceItemAppService : ApplicationService, IInvoiceItemsAppService
    {
        private readonly IInvoiceItemRepository _invoiceItemRepository;
       
        private readonly IInvoiceRepository _invoice;
        private readonly IProductRepository _productRepo;
        private readonly IDataFilter _dataFilter;

        public InvoiceItemAppService(IProductRepository productRepo, IInvoiceRepository invoice,  IInvoiceItemRepository invoiceItemRepository, IDataFilter dataFilter)
        {
         
           
            _invoice = invoice;
            _productRepo = productRepo;
            _invoiceItemRepository = invoiceItemRepository;
            _dataFilter = dataFilter;
        }
        public async Task CreateAsync(List<CreateInvoiceItemsDto> input, Guid invouceId)
        {
            var productquery = await _productRepo.GetAll();
            var products = ObjectMapper.Map<List<Product>, List<ProductsDto>>(productquery);
            var query = input.Join(products, x => x.ProductId, a => a.Id, (i, p) => new InvoiceItem
            {
                Quantity = i.Quantity,
                InvoiceId = invouceId,
                ProductId = p.Id,
                PriceId = p.PriceDetails.Id,
                Price = p.PriceDetails.Price,
                TotalPrice = p.PriceDetails.Price * i.Quantity,
                DiscountId = p.DiscountDetails.Id,
                TotalDiscount = p.DiscountDetails != null ? (p.PriceDetails.Price * p.DiscountDetails.Discount / 100) * i.Quantity : 0,
                TotalNet = (p.PriceDetails.Price * i.Quantity) - (p.DiscountDetails != null ? (p.PriceDetails.Price * p.DiscountDetails.Discount / 100) * i.Quantity : 0)




            }).ToList();
            await _invoiceItemRepository.InsertManyAsync(query, autoSave: true);
         await AddOrDeleteItemFromToInvoice(invouceId,query.Sum(x=>x.TotalPrice), query.Sum(x => x.TotalDiscount), query.Sum(x => x.TotalNet));

            return;

        }

        public async Task DeleteAsync(Guid id)
        {
           var invoiceitem = await _invoiceItemRepository.GetAsync(id);
            if (invoiceitem == null)
            { 
                throw new Exception("No invoiceItem with this id"); 
            }
            await AddOrDeleteItemFromToInvoice(invoiceitem.InvoiceId, -invoiceitem.TotalPrice, -invoiceitem.TotalDiscount,-invoiceitem.TotalNet);
            await _invoiceItemRepository.DeleteAsync(invoiceitem);
           
        }

        public async Task<InvoiceItemsDto> GetAsync(Guid id)
        {
            var invoiceitem = await _invoiceItemRepository.GetAsync(id,includeDetails: true);
            if (invoiceitem == null)
            {
                throw new Exception("No invoiceItem with this id");
            }
            return ObjectMapper.Map<InvoiceItem, InvoiceItemsDto>(invoiceitem);
        }

        public async Task<PagedResultDto<InvoiceItemsDto>> GetListAsync(GetInvoiceItemListDto input)
        {

            if (input.Sorting == null)
            {
                input.Sorting = nameof(InvoiceItem.CreationTime);
            }
            var query = await _invoiceItemRepository.GetQueryableAsync();
            query = query.OrderBy(input.Sorting);
                    

            var invoices = await query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();
            var totalCount = await _invoiceItemRepository.CountAsync();
            return new PagedResultDto<InvoiceItemsDto>(totalCount, ObjectMapper.Map<List<InvoiceItem>, List<InvoiceItemsDto>>(invoices));

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

                var product = await _productRepo.GetProduct(id);
                
                if (product == null)
                {
                    throw new Exception("There is no product with this Id");
                }
                var mappedObject = ObjectMapper.Map<Product, ProductsDto>(product);
                decimal discount = invoiceitem.TotalPrice * mappedObject.DiscountDetails.Discount / 100;


                invoiceitem.PriceId = mappedObject.PriceDetails.Id;
                invoiceitem.Price = mappedObject.PriceDetails.Price;
                invoiceitem.DiscountId = mappedObject.DiscountDetails.Id;
                invoiceitem.TotalDiscount = discount;
                invoiceitem.TotalPrice = mappedObject.PriceDetails.Price * input.Quantity;
                invoiceitem.TotalNet = invoiceitem.TotalPrice - discount;


            }
            var updated= await _invoiceItemRepository.UpdateAsync(invoiceitem,autoSave: true);
            return ObjectMapper.Map<InvoiceItem, InvoiceItemsDto>(updated);
        }
        [Authorize(InvoiceSystemPermissions.Invoices.RestoreDelete)]
        public async Task RestoreDelete(Guid id)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var item = await _invoiceItemRepository.GetAsync(id);
                if (item != null && item.IsDeleted )
                {
                    item.IsDeleted = false;
                    await _invoiceItemRepository.UpdateAsync(item, autoSave: true);
                    return;
                }
                
                    throw new Exception();
                
             
            }
        }
        private  async Task  AddOrDeleteItemFromToInvoice (Guid id,decimal totalprice,decimal totaldiscount,decimal totalnet)
        {
            var invoice = await _invoice.GetAsync(id);
            if (invoice == null)
            {
                throw new Exception("no invoice found");
            }
            
            invoice.InvoiceAmount +=  totalprice;
            invoice.TotalDiscount += totaldiscount;
            invoice.NetAmount +=totalnet;
            await _invoice.UpdateAsync(invoice,autoSave: true);
            



        }
       /* private async Task RemoveOtemFromInvoice(Guid id, decimal totalprice, decimal net)
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
            


        }*/
        
    }
}

