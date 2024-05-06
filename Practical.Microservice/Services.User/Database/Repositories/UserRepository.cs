using Microsoft.EntityFrameworkCore;
using Services.Users.Database.Entities;
using UserManagement.Database.DbContexts;
using UserManagement.Database.Interfaces;
using UserManagement.Models;

namespace UserManagement.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserMasterDbContext userMasterDbContext;
        private readonly UserSlaveDbContext userSlaveDbContext;

        public UserRepository(UserMasterDbContext userMasterDbContext, UserSlaveDbContext userSlaveDbContext)
        {
            this.userMasterDbContext = userMasterDbContext;
            this.userSlaveDbContext = userSlaveDbContext;
        }

        public async Task<User> AddUserAsync(AddUserModel data)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = data.Name,
                DateOfBirth = data.DateOfBirth,
                Email = data.Email,
                Note = ""
            };
            userMasterDbContext.Add(newUser);
            await userMasterDbContext.SaveChangesAsync();

            return new User
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                DateOfBirth = newUser.DateOfBirth,
                Note = newUser.Note
            };
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await userSlaveDbContext.Users.ToListAsync();
            return users.Select(newUser => new User
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                DateOfBirth = newUser.DateOfBirth,
                Note = newUser.Note
            }).ToList();
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            var user = await userSlaveDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null)
            {
                throw new Exception("User is not found.");
            }
            return new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Note = user.Note
            };
        }
    }
}
