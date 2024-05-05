using Microsoft.EntityFrameworkCore;
using MovieApi.DBContext;
using MovieApi.DTO.ResponseTO;
using MovieApi.Models;
using MovieApi.Services.Interface;
using System.Collections.Generic;

namespace MovieApi.Services
{
    public class MovieService(MovieServiceContext dbContext) : IMovieService
    {
        public async Task<CrewResponseTo[]> GetCrewForFilm(int id)
        {
            try
            {

            }
            catch
            {

            }
            throw new NotImplementedException();
        }

        public async Task<MovieResponseTo> GetFilmById(int id)
        {
            try
            {
                var film = await dbContext.FilmInfos.FirstOrDefaultAsync(f => f.FilmId == id);

                var response = new MovieResponseTo()
                {
                    Age = film!.Age,
                    CountryName = film.CountryName,
                    Description = film.Description,
                    ReleaseDate = film.ReleaseDate,
                    FilmPath = film.FilmPath,
                    Id = film.FilmId,
                    Name = film.Name,
                    PosterPath = film.PosterPath,
                    TrailerPath = film.TrailerPath
                };

                return response;

            }
            catch (Exception ex)
            {
                return null!;
            }
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
                    response.Add(new MovieResponseTo()
                    {
                        Age = films[i].Age,
                        CountryName = films[i].CountryName,
                        Description = films[i].Description,
                        ReleaseDate = films[i].ReleaseDate,
                        FilmPath = films[i].FilmPath,
                        Id = films[i].FilmId,
                        Name = films[i].Name,
                        PosterPath = films[i].PosterPath,
                        TrailerPath = films[i].TrailerPath
                    });
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
