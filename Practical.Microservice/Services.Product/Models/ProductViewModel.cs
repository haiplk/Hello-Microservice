namespace Services.Product.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public DateTime? CreatedDated { get; set; }

        public Guid? CreatedUserId { get; set; }

        public UserInfoViewModel? CreatedUser { get; set; }
    }
}
