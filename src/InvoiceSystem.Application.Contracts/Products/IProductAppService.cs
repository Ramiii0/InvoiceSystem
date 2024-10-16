﻿
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
        Task<ProductDto> GetAsync(Guid id);

        Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input);

        Task<ProductDto> CreateAsync(CreateProductDto input);

        Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto input);

        Task DeleteAsync(Guid id);

    }
}
