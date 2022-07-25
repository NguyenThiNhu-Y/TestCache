using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using TestCache.BookCacheItems;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace TestCache.Books
{
    public class BookService:ITransientDependency
    {
        private readonly IDistributedCache<BookCacheItem> _cache;
        private readonly IBookRepository _bookRepository;

        public BookService(IDistributedCache<BookCacheItem> cache, IBookRepository bookRepository)
        {
            _cache = cache;
            _bookRepository = bookRepository;

        }

       
        public async Task<BookCacheItem> GetAsync(Guid bookId)
        {
            return await _cache.GetOrAddAsync(
                bookId.ToString(), //Cache key
                async () => await GetBookFromDatabaseAsync(bookId),
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                }
            );
        }

        private async Task<BookCacheItem> GetBookFromDatabaseAsync(Guid bookId)
        {
            var book = await _bookRepository.FindAsync(bookId);
            return new BookCacheItem()
            {
                Name = book.Name,
                Price = book.Price
            };
        }
    }
}
