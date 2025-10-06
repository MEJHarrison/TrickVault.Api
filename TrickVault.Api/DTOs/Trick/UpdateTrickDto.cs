using System.ComponentModel.DataAnnotations;

namespace TrickVault.Api.DTOs.Trick
{
    public class UpdateTrickDto : CreateTrickDto
    {
        [Required]
        public int Id { get; set; }
    }
}
