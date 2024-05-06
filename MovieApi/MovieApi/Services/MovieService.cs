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
                var result = new List<CrewResponseTo>();
                var query = from filmCrew in dbContext.FilmCrews
                            join crewRole in dbContext.CrewRoles on filmCrew.FcrRole equals crewRole.CrId
                            join person in dbContext.People on filmCrew.FcrPerson equals person.PerId
                            where filmCrew.FcrFilm == id
                            select new
                            {
                                CrewId = filmCrew.FcrId,
                                CrewRoleName = crewRole.CrName,
                                PersonName = person.PerName,
                                PersonSurname = person.PerSurname
                            };

                var queryResult = await query.ToListAsync();
                foreach (var q in queryResult)
                {
                    result.Add(new CrewResponseTo()
                    {
                        Id = q.CrewId,
                        Name = q.PersonName,
                        Role = q.CrewRoleName,
                        Surname = q.PersonSurname
                    });
                }
                return result.ToArray();
            }
            catch
            {
                return null!;
            }
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

        public async Task<GenresResponseTo[]> GetGenresForFilm(int id)
        {
            try
            {
                var gnr_names = dbContext.Films
                            .Where(f => f.FlmId == id)
                            .Include(f => f.FgGenres)
                            .SelectMany(f => f.FgGenres.Select(g => g.GnrName))
                            .ToList();

                var result = new List<GenresResponseTo>();
                foreach (var gen in gnr_names)
                {
                    result.Add(new GenresResponseTo()
                    {
                        Name = gen
                    });
                }
               

                return [..result];

            }
            catch (Exception ex)
            {
                return null!;
            }
        }
    }
}
