using MovieApi.DTO.ResponseTO;
using System.Security.Cryptography;

namespace MovieApi.Services.Interface
{
    public interface IUserService
    {
        public Task<UserResponseTo> RegistrateUser(string email, string password, string name);
        public Task<UserResponseTo> AuthorizeUser(string email, string password);
    }
}
