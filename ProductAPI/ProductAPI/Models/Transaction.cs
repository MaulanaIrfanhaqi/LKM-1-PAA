namespace ProductAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Product? Product { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}