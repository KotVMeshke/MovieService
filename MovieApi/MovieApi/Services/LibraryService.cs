using Microsoft.EntityFrameworkCore;
using MovieApi.DBContext;
using MovieApi.DTO.ResponseTO;
using MovieApi.Models;
using MovieApi.Services.Interface;
using System.Collections.Generic;

namespace MovieApi.Services
{
    public class LibraryService(MovieServiceContext dbContext) : ILibraryService
    {
        public async Task<bool> AddFilmIntoLibrary(int userId, int filmId)
        {

            try
            {
                var film = await dbContext.Films.FindAsync(filmId);
                var user = await dbContext.Users.FindAsync(userId);
                if (film == null || user == null)
                    throw new Exception();
                user.LibFilms.Add(film);
                film.LibUsers.Add(user);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<LibraryResponseTo[]> GetLibrary(int userId)
        {
            var response = new List<LibraryResponseTo>();
            try
            {
                var filmIds = await dbContext.Users
                         .Where(u => u.UsrId == userId)
                         .SelectMany(u => u.LibFilms.Select(f => f.FlmId))
                         .ToListAsync();

                var films = await dbContext.FilmInfos
                            .Where(v => filmIds.Contains(v.FilmId))
                            .ToListAsync();
                for (int i = 0; i < films.Count; i++)
                {
                    response.Add(new LibraryResponseTo()
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

        public async Task<bool> RemoveFilmFromLibrary(int userId, int filmId)
        {
            try
            {
                var film = await dbContext.Films
                        .Include(f => f.LibUsers)
                        .FirstOrDefaultAsync(f => f.FlmId == filmId);
                var user = await dbContext.Users
                        .Include(u => u.LibFilms)
                        .FirstOrDefaultAsync(u => u.UsrId == userId);
                if (film == null || user == null)
                    throw new Exception();
                user.LibFilms.Remove(film);
                film.LibUsers.Remove(user);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
