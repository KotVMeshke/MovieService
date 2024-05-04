using Microsoft.EntityFrameworkCore;
using MovieApi.DBContext;
using MovieApi.DTO.ResponseTO;
using MovieApi.Models;
using MovieApi.Services.Interface;

namespace MovieApi.Services
{
    public class MovieService(MovieServiceContext dbContext) : IMovieService
    {
        public async Task<MovieResponseTo> GetFilmById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<MovieResponseTo[]> GetFilms(int offset, int limit)
        {
            var response = new List<MovieResponseTo>(limit);
            try
            {
                var films = await dbContext.FilmInfos
                                        .Skip(offset)
                                        .Take(limit)
                                        .ToListAsync();
                for (int i = 0; i < films.Count; i++)
                {
                    response.Add(new MovieResponseTo() { 
                        Age = films[i].Age, 
                        CountryName = films[i].CountryName,
                        Description = films[i].Description, 
                        ReleaseDate = films[i].ReleaseDate, 
                        FilmPath = films[i].FilmPath,
                        Id = films[i].FilmId,
                        Name = films[i].Name,
                        PosterPath = films[i].PosterPath,
                        TrailerPath = films[i].TrailerPath});
                }
                return [.. response];

            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
