using image_api_netcore.src.Models;
using image_api_netcore.src.Repositories.Interfaces;
using image_api_netcore.src.Data;
using image_api_netcore.src.DTOs;
using Microsoft.EntityFrameworkCore;

namespace image_api_netcore.src.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.email == email);
            return user ?? throw new Exception("Usuario no encontrado");
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<bool> VerifyEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.email == email);
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}