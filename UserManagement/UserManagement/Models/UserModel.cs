
namespace UserManagement.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
