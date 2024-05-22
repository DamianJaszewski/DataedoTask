using DataedoTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DataedoTask.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext Context)
        {
            _context = Context;
        }

        public async Task DeleteUser(User user)
        {
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while deleting the user: {ex.Message}");
                throw new Exception("An error occurred while deleting the user.", ex);
            }
        }

        public async Task<User> GetUserByIdAsync(uint id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }
    }
}
