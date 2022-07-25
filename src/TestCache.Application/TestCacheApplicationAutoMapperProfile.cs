using AutoMapper;
using TestCache.BookCacheItems;
using TestCache.Books;

namespace TestCache
{
    public class TestCacheApplicationAutoMapperProfile : Profile
    {
        public TestCacheApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Book, BookDto>();
            CreateMap<CreateOrUpdateBook, Book>();
            CreateMap<Book, BookCacheItem>();

        }
    }
}
