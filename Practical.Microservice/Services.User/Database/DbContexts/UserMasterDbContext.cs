using Microsoft.EntityFrameworkCore;
using UserManagement.Database.Models;
using UserManagement.Services;

namespace UserManagement.Database.DbContexts
{
    public class UserMasterDbContext : DbContext
    {

        public UserMasterDbContext(DbContextOptions<UserMasterDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

    }
}
