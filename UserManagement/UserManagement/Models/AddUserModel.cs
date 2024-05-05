namespace UserManagement.Models
{
    public class AddUserModel
    {

        public required string Name { get; set; }

        public string? Email { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
