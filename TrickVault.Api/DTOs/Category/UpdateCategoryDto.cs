using System.ComponentModel.DataAnnotations;

namespace TrickVault.Api.DTOs.Category
{
    public class UpdateCategoryDto : CreateCategoryDto
    {
        [Required]
        public int Id { get; set; }
    }
}
