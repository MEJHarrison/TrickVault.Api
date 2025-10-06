namespace TrickVault.Api.DTOs.Category
{
    public record GetCategoryDto(
        int Id,
        string Name,
        string? Description
    );
}
