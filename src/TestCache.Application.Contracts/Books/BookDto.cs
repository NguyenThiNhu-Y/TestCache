using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace TestCache.Books
{
    public class BookDto: AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
