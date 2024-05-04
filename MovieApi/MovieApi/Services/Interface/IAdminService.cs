namespace MovieApi.Services.Interface
{
    public interface IAdminService
    {
        public Task<bool> BanUser(int userId, int adminId);
        public Task<bool> UnBanUser(int userId, int adminId);
    }
}
