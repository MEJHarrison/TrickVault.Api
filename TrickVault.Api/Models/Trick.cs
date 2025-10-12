using System.ComponentModel.DataAnnotations.Schema;

namespace TrickVault.Api.Models
{
    public class Trick
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Effect { get; set; }
        public string? Setup { get; set; }
        public string? Method { get; set; }
        public string? Patter { get; set; }
        public string? Comments { get; set; }
        public string? Credits { get; set; }

        public ICollection<TrickCategory> TrickCategories { get; set; } = new List<TrickCategory>();

        [NotMapped]
        public List<int> CategoryIds { get; set; } = new();

        public required string UserId { get; set; }
        public required ApplicationUser User { get; set; }
    }
}
