using UserManagement.Database.Interfaces;
using UserManagement.Models;

namespace UserManagement.Services.Impls
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserModel> AddUserAsync(AddUserModel data)
        {
            var newUser = await  userRepository.AddUserAsync(data);

            return new UserModel
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                DateOfBirth = newUser.DateOfBirth
            };
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            var users = await userRepository.GetAllAsync();
            return users.Select(newUser => new UserModel
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                DateOfBirth = newUser.DateOfBirth
            }).ToList();
        }

        public async Task<UserModel> GetUserAsync(Guid userId)
        {
            var user = await userRepository.GetUserAsync(userId);
            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };
        }
    }
}
