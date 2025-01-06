using image_api_netcore.src.Models;
using image_api_netcore.src.DTOs;

namespace image_api_netcore.src.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();

        Task<bool> VerifyEmail(string email);

        Task<User> AddUser(User user);

        Task<User> GetUserByEmail(string email);
    }
}