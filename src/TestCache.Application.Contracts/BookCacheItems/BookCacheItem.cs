using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace TestCache.BookCacheItems
{
    [CacheName("Books")]
    public class BookCacheItem
    {
        public string Name { get; set; }

        public int Price { get; set; }
    }
}
