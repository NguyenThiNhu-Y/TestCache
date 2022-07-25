using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCache.BookCacheItems;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TestCache.Books
{
    public interface IBookAppService : IApplicationService  
    {
        Task<BookDto> GetBookAsync(Guid bookId);

        Task<PagedResultDto<BookDto>> GetListAsync(GetInputBook input);

        Task<BookDto> Create(CreateOrUpdateBook input);
        Task<List<BookDto>> CreateMany(List<CreateOrUpdateBook> input);
        Task<List<BookDto>> CreateManyAuto();

        Task<List<BookDto>> FindBookByPrice(GetInputBook input);


    }
}
