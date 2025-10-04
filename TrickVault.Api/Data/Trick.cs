namespace TrickVault.Api.Data
{
    public class Trick
    {
        public int Id { get; set; }
        //public Classification classification { get; set; }
        public required string Title { get; set; }
        public string? Effect { get; set; }
        public string? Setup { get; set; }
        public string? Method { get; set; }
        public string? Patter { get; set; }
        public string? Comments { get; set; }
        public string? Credits { get; set; }
    }
}
