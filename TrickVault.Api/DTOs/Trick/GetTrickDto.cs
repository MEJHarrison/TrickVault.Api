using TrickVault.Api.DTOs.Category;

namespace TrickVault.Api.DTOs.Trick
{
    public record GetTrickDto
    {
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string? Effect { get; init; }
        public string? Setup { get; init; }
        public string? Method { get; init; }
        public string? Patter { get; init; }
        public string? Comments { get; init; }
        public string? Credits { get; init; }

        public List<GetCategoryDto> Categories { get; init; } = new List<GetCategoryDto>();
    };
}
