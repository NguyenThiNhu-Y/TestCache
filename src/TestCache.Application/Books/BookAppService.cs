using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TestCache.BookCacheItems;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;


namespace TestCache.Books
{
    public class BookAppService : ApplicationService, IBookAppService
    {
        private readonly IDistributedCache<BookCacheItem> _cache;

        private readonly IBookRepository _bookRepository;
        public BookAppService(IDistributedCache<BookCacheItem> cache, IBookRepository bookRepository)
        {
            _cache = cache;

            _bookRepository = bookRepository;
        }
        public async Task<BookCacheItem> GetCacheAsync(Guid bookId)
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

        public async Task<BookCacheItem> GetCache1Async(Guid bookId)
        {
            return await _cache.GetAsync(bookId.ToString());
        }
        public async Task<KeyValuePair<string, BookCacheItem>[]> GetListCachePriceThan100Async(int price, GetInputBook input)
        {
            var listGuid = GetListGuidThan100(price, input).Result.AsEnumerable();
            List<KeyValuePair<string, BookCacheItem>> result = await GetListCachePriceThan100FromDatabaseAsync(price, input);
            var rs = await _cache.GetOrAddManyAsync(
                listGuid,
                  (m) =>
                {
                    return Task.FromResult(result);
                },


                //await GetListCachePriceThan100FromDatabaseAsync(listGuid, price),
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                });
            return rs;
        }

        public async Task<KeyValuePair<string, BookCacheItem>[]> GetListByPriceInCache(int price, GetInputBook input)
        {
            var listGuid = GetListGuidThan100(price, input).Result.AsEnumerable();
            //IServiceCollection services = new ServiceCollection();
            //var provider = services.BuildServiceProvider();
            //var bookCache = provider.GetRequiredService<IDistributedCache<BookCacheItem>>();
            //var testKey = new[] {price.ToString()};
            //List<KeyValuePair<string, BookCacheItem>> result = await GetListCachePriceThan100FromDatabaseAsync(price, input);
            //var books = await _bookRepository.GetListAsync();
            //var bookCacheItems = ObjectMapper.Map<List<Book>, List<BookCacheItem>> (books);
            //var cacheValue = await _cache.GetOrAddManyAsync(
            //    testKey,
            //    (missingKeys) => Task.FromResult(new List<KeyValuePair<string, BookCacheItem>>
            //{
            //    new("100", bookCacheItems.ToArray()[0])
            //})
            //);
            var cacheValue = await _cache.GetManyAsync(listGuid);
            return cacheValue;
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

        private async Task<List<string>> GetListGuidThan100(int price, GetInputBook input)
        {
            var books = await _bookRepository.GetListAsync(input.filter, input.SkipCount, input.MaxResultCount, input.Sorting);
            return books.Select(x=>x.Id.ToString()).ToList();
        }
        //private async Task<(IEnumerable<string> , Task<List<KeyValuePair<string,BookCacheItem>>>)> GetListCachePriceThan100FromDatabaseAsync(IEnumerable<string> listGuid, int price)
        //{
        //    var books = await _bookRepository.GetListAsync();
        //    List<KeyValuePair<string, BookCacheItem>> result = new List<KeyValuePair<string, BookCacheItem>>();
        //    foreach (var item in books)
        //    {
        //        if (item.Price==price)
        //        {
        //            KeyValuePair<string, BookCacheItem> rs =
        //                new KeyValuePair<string, BookCacheItem>(item.Id.ToString(), ObjectMapper.Map<Book, BookCacheItem>(item));
        //        }
        //    }
        //    return (listGuid, Task.FromResult(result));
        //}
        private async Task<List<KeyValuePair<string, BookCacheItem>>> GetListCachePriceThan100FromDatabaseAsync( int price, GetInputBook input)
        {
            var books = await _bookRepository.GetListAsync(input.filter, input.SkipCount, input.MaxResultCount, input.Sorting);
            var rs = books.Select(x=>new KeyValuePair<string, BookCacheItem>(x.Id.ToString(), new BookCacheItem{Name = x.Name, Price = x.Price}));
            //List<KeyValuePair<string, BookCacheItem>> result = new List<KeyValuePair<string, BookCacheItem>>();
            //foreach (var item in books)
            //{
            //    KeyValuePair<string, BookCacheItem> rs =
            //        new KeyValuePair<string, BookCacheItem>(item.Id.ToString(), ObjectMapper.Map<Book, BookCacheItem>(item));
            //    result.Add(rs);
            //}
            //return await _bookRepository.GetListKeyValuePairAsync(input.filter, input.SkipCount, input.MaxResultCount, input.Sorting);
            return rs.ToList();
        }
        //private async Task<List<BookCacheItem>> GetListCachePriceThan100FromDatabaseAsync(IEnumerable<string> listGuid, int price)
        //{
        //    var books = await _bookRepository.GetListAsync();
        //    List<BookCacheItem> result = new List<BookCacheItem>();
        //    foreach (var item in books)
        //    {
        //        if (item.Price == price)
        //        {

        //            result.Add(new BookCacheItem
        //            {
        //                Name = item.Name,
        //                Price = item.Price
        //            });
        //        }
        //    }
        //    return result;
        //}

        public async Task<PagedResultDto<BookDto>> GetListAsync(GetInputBook input)
        {
            var totalCount = 
                await _bookRepository.CountAsync();
            var books = await _bookRepository.GetListAsync(input.filter, input.SkipCount, input.MaxResultCount, input.Sorting);
            return new PagedResultDto<BookDto>(
                totalCount,
                ObjectMapper.Map<List<Book>, List<BookDto>>(books)
            );
        }

        public async Task<BookDto> GetBookAsync(Guid bookId)
        {
            var book = await _bookRepository.FindAsync(bookId);
            return ObjectMapper.Map<Book, BookDto>(book);
        }

        public async Task<BookDto> Create(CreateOrUpdateBook input)
        {
            var book = ObjectMapper.Map<CreateOrUpdateBook, Book>(input);
            await _bookRepository.InsertAsync(book);
            return ObjectMapper.Map<Book, BookDto>(book);
        }

        public async Task<List<BookDto>> CreateMany(List<CreateOrUpdateBook> input)
        {
            var books = ObjectMapper.Map<List<CreateOrUpdateBook>, List<Book>>(input);
            await _bookRepository.InsertManyAsync(books);
            return ObjectMapper.Map<List<Book>, List<BookDto>>(books);
        }

        public async Task<List<BookDto>> CreateManyAuto()
        {
            List<BookDto> books = new List<BookDto>();
            var totalCount =
                await _bookRepository.CountAsync();
            for (int i = totalCount + 1; i <= totalCount + 1000; i++)
            {
                Book book = new Book()
                {
                    Name = "book" + i,
                    Price = 100
                };
                await _bookRepository.InsertAsync(book);
                books.Add(ObjectMapper.Map<Book, BookDto>(book));
            }

            return books;
        }

        public async Task<List<BookDto>> FindBookByPrice(GetInputBook input)
        {
            var books = await _bookRepository.GetListAsync(input.filter, input.SkipCount, input.MaxResultCount, input.Sorting);
            List<BookDto> result = new List<BookDto>();
            //foreach (var item in books)
            //{
            //    if (item.Price == price)
            //    {
                    
            //        result.Add(ObjectMapper.Map<Book, BookDto>(item));
            //    }
            //}
            return result;
        }
    }
}
