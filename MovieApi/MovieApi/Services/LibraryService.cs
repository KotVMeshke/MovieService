using MovieApi.DBContext;
using MovieApi.Services.Interface;

namespace MovieApi.Services
{
    public class LibraryService(MovieServiceContext dbContext) : ILibraryService
    {
    }
}
