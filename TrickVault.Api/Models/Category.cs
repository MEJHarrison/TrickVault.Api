using System.Text.Json.Serialization;

namespace TrickVault.Api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<TrickCategory> TrickCategories { get; set; } = new List<TrickCategory>();
    }
}
