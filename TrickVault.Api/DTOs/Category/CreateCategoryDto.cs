using System.ComponentModel.DataAnnotations;

namespace TrickVault.Api.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
