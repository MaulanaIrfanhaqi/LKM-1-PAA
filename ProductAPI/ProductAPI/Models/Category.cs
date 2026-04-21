using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductAPI.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [JsonIgnore] // 🔥 penting
        public ICollection<Product>? Products { get; set; }
    }
}