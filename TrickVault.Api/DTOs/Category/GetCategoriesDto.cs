namespace TrickVault.Api.DTOs.Category
{
    public record GetCategoriesDto(
        int Id,
        string Name,
        string? Description
    );
}
