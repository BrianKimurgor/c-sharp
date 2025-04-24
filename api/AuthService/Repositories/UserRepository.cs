// File: Repositories/UserRepository.cs
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthService.Data;
using AuthService.Models;

namespace AuthService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _ctx;
        public UserRepository(AuthDbContext ctx) => _ctx = ctx;

       public Task<User> GetUserByIdAsync(int userId)
        {
            return _ctx.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _ctx.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _ctx.Users.AddAsync(user);
            await SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _ctx.Users.Update(user);
            await SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                _ctx.Users.Remove(user);
                await SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
