using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCache.BookCacheItems;
using Volo.Abp.Domain.Repositories;

namespace TestCache.Books
{
    public interface IBookRepository:IRepository<Book, Guid>
    {
        Task<List<Book>> GetListAsync(
            int filter,
            int skipCount,
            int maxResultCount,
            string sorting
        );

        
        Task<List<KeyValuePair<string, BookCacheItem>>> GetListKeyValuePairAsync(int filter, int skipCount,
            int maxResultCount, string sorting);
    }
}
