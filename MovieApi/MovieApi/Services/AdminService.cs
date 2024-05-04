using MovieApi.DBContext;
using MovieApi.Services.Interface;

namespace MovieApi.Services
{
    public class AdminService(MovieServiceContext dbContext) : IAdminService
    {
        public async Task<bool> BanUser(int userId, int adminId)
        {
            try
            {
                var user = await dbContext.Users.FindAsync(userId);
                var admin = await dbContext.Users.FindAsync(adminId);
                if (user == null || admin == null || admin.UserRole != 2)
                    throw new Exception();
                user.UserBannedBy = admin.UsrId;
                dbContext.Update(user);
                await dbContext.SaveChangesAsync();
                  
                return true;

            }catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UnBanUser(int userId, int adminId)
        {
            try
            {
                var user = await dbContext.Users.FindAsync(userId);
                var admin = await dbContext.Users.FindAsync(adminId);
                if (user == null || admin == null || admin.UserRole != 2)
                    throw new Exception();
                user.UserBannedBy = null;
                dbContext.Update(user);
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
