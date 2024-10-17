using AutoMapper;
using InvoiceSystem.InvoiceItems;
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
            CreateMap<CreateInvoiceItemsDto, InvoiceItem>();
                
            CreateMap<InvoiceItem, InvoiceItemsDto>();
            CreateMap<UpdateInvoiceItemsDto, InvoiceItem>()
                .ForMember(x => x.ProductId, opt => opt.Condition(src => src.ProductId != Guid.Empty))
                 .ForMember(x => x.InvoiceId, opt => opt.Condition(src => src.InvoiceId != Guid.Empty));



        }
    }
}
