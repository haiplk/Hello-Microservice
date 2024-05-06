namespace UserManagement.Database.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Note { get; set; }
    }
}
