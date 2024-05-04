using Microsoft.EntityFrameworkCore;
using MovieApi.DBContext;
using MovieApi.DTO.ResponseTO;
using MovieApi.Models;
using MovieApi.Services.Interface;
using System.Security.Cryptography;
using System.Text;

namespace MovieApi.Services
{
    public class UserService(MovieServiceContext dbContext) : IUserService
    {
        public async Task<UserResponseTo> AuthorizeUser(string email, string password)
        {
            var response = new UserResponseTo();
            try
            {
                var hash = CreateHashCode(password);
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UsrPassword == hash && x.UsrEmail == email);
                if (user == null)
                {
                    throw new Exception();
                }
                response.UserID = user.UsrId;
                response.Status = user.UserBannedBy == null ? "alive" : "banned";
                response.UserName = user.UsrName;
                response.IsAdmin = user.UserRole == 2;

            }
            catch (Exception ex)
            {
                response.UserID = -1;
            }

            return response;
        }

        public async Task<UserResponseTo> RegistrateUser(string email, string password, string name)
        {
            var response = new UserResponseTo();
            try
            {
                var hash = CreateHashCode(password);
                var user = new User() { UserRole = 1, UsrName = name, UsrPassword = hash, UsrEmail = email };
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
                response.UserID = user.UsrId;
                response.Status = user.UserBannedBy != null ? "alive" : "banned";
                response.UserName = user.UsrName;
                response.IsAdmin = user.UserRole == 2;
                
            }
            catch (Exception ex)
            {
                response.UserID = -1;
            }

            return response;
            
        }

        private string CreateHashCode(string input)
        {
            string hash = string.Empty;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                hash = builder.ToString();
            }
            return hash;
        }
    }
}
