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

        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
