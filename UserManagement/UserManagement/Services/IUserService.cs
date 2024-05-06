using UserManagement.Models;

namespace UserManagement.Services
{
    public interface IUserService
    {
        Task<UserModel> AddUserAsync(AddUserModel data);

        Task<UserModel> GetUserAsync(Guid userId);

        Task<List<UserModel>> GetAllAsync();
    }
}
