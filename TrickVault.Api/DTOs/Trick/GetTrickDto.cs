using TrickVault.Api.DTOs.Category;

namespace TrickVault.Api.DTOs.Trick
{
    public record GetTrickDto(
        int Id,
        string Title,
        string? Effect,
        string? Setup,
        string? Method,
        string? Patter,
        string? Comments,
        string? Credits,

        List<GetCategoryDto> Categories
    );
}
