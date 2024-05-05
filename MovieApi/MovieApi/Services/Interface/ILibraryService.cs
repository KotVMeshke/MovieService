using MovieApi.DTO.ResponseTO;

namespace MovieApi.Services.Interface
{
    public interface ILibraryService
    {
        public Task<bool> AddFilmIntoLibrary(int userId,int filmId);
        public Task<bool> RemoveFilmFromLibrary(int userId,int filmId);
        public Task<LibraryResponseTo[]> GetLibrary(int userId);
    }
}
