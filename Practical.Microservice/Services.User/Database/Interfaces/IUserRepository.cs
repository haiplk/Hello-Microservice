using UserManagement.Database.Models;
using UserManagement.Models;

namespace UserManagement.Database.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(AddUserModel data);

        Task<User> GetUserAsync(Guid userId);

        Task<List<User>> GetAllAsync();
    }
}
