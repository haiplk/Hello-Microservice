﻿namespace Services.Product.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public DateTime? CreatedDated { get; set; }

        public Guid? CreatedUserId { get; set; }
    }
}
