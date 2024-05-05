using MovieApi.DTO.ResponseTO;

namespace MovieApi.Services.Interface
{
    public interface IMovieService
    {
        public Task<MovieResponseTo[]> GetFilms(int offset, int limic);
        public Task<MovieResponseTo> GetFilmById(int id);
        public Task<CrewResponseTo[]> GetCrewForFilm(int id);
    }
}
