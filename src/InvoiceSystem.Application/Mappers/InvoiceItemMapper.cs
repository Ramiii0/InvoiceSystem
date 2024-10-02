using AutoMapper;
using InvoiceSystem.InvoiceItem;
using InvoiceSystem.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Mappers
{
    public class InvoiceItemMapper:Profile
    {
        public InvoiceItemMapper()
        {
            CreateMap<CreateInvoiceItemsDto, InvoiceItems>();
            CreateMap<InvoiceItems, InvoiceItemsDto>();
            CreateMap<UpdateInvoiceItemsDto, InvoiceItems>();
            
            
        }
    }
}
