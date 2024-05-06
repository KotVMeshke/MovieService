using MovieApi.DTO.ResponseTO;

namespace MovieApi.Services.Interface
{
    public interface ICrewService
    {
        public Task<FullCrewResponceTo> GetCrewById(int id);
    }
}
