using AutoMapper;
using InvoiceSystem.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Mappers
{
    public class InvoiceMapper : Profile
    {
        public InvoiceMapper()
        {
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceDto, Invoice>();
            CreateMap<CreateInvoiceDto, InvoiceDto>();
            CreateMap<CreateInvoiceDto, Invoice>();
            CreateMap<UpdateInvoiceDto, InvoiceDto>();
            CreateMap<UpdateInvoiceDto, Invoice>();
            CreateMap<InvoiceDto, UpdateInvoiceDto>();

        }
    }
}
