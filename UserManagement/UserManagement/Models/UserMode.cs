
namespace UserManagement.Models
{
    public class UserMode
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Email { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
