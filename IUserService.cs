using DataedoTask.Models;

namespace DataedoTask.Services
{
    public interface IUserService
    {
        Task <User> GetUserByIdAsync(uint id);
        Task DeleteUser(User user);
    }
}
