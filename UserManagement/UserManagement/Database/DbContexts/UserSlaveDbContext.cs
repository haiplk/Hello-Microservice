
using Microsoft.EntityFrameworkCore;
using UserManagement.Database.Models;
using UserManagement.Services;

namespace UserManagement.Database.DbContexts
{
    public class UserSlaveDbContext : DbContext
    {
        public UserSlaveDbContext(DbContextOptions<UserSlaveDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
    }
}
