namespace TrickVault.Api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Trick> Tricks { get; set; } = new List<Trick>();
    }
}
