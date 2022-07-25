using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TestCache.BookCacheItems;
using TestCache.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace TestCache.Books
{
    public class MongodbBookRepository: MongoDbRepository<TestCacheMongoDbContext, Book, Guid>,IBookRepository
    {
        public MongodbBookRepository(
            IMongoDbContextProvider<TestCacheMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
        {
        }

        public async Task<List<Book>> GetListAsync(int filter, int skipCount, int maxResultCount, string sorting)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<Book, IMongoQueryable<Book>>(
                    filter!=0,
                    book => book.Price==filter
                )
                .As<IMongoQueryable<Book>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        

        public async Task<List<KeyValuePair<string, BookCacheItem>>> GetListKeyValuePairAsync(int filter, int skipCount, int maxResultCount, string sorting)
        {
            var queryable = await GetMongoQueryableAsync();
            var books = await queryable
                .WhereIf<Book, IMongoQueryable<Book>>(
                    filter != 0,
                    book => book.Price == filter
                )
                .As<IMongoQueryable<Book>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
            List<KeyValuePair<string, BookCacheItem>> result = new List<KeyValuePair<string, BookCacheItem>>();
            foreach (var item in books)
            {
                KeyValuePair<string, BookCacheItem> rs =
                    new KeyValuePair<string, BookCacheItem>(item.Id.ToString(), new BookCacheItem{Name = item.Name, Price = item.Price});
                result.Add(rs);
            }
            return result;
        }
    }
}
