namespace TrickVault.Api.Models
{
    public class TrickCategory
    {
        public int TrickId { get; set; }
        public Trick Trick { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
