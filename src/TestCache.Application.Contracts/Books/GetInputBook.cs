using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace TestCache.Books
{
    public class GetInputBook : PagedAndSortedResultRequestDto
    {
        public int filter { get; set; }
    }
}
