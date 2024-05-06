using Microsoft.EntityFrameworkCore;
using MovieApi.DBContext;
using MovieApi.DTO.ResponseTO;
using MovieApi.Services.Interface;

namespace MovieApi.Services
{
    public class CrewService(MovieServiceContext dbContext) : ICrewService
    {
        public async Task<FullCrewResponceTo> GetCrewById(int id)
        {
            try
            {

                var person = await dbContext.People
                                    .Include(p => p.Photos)
                                    .Where(p=> p.PerId == id)
                                    .ToListAsync();

                var result = new FullCrewResponceTo()
                {
                    Age = person[^1].PerAge,
                    Patronymic = person[^1].PerPatronymic,
                    Surname = person[^1].PerSurname,
                    Name = person[^1].PerName,
                    Date = person[^1].PerBirthDate,
                    PhotoPath = person[^1].Photos.FirstOrDefault()!.PhPath
                };


               
                return result;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }
    }
}
