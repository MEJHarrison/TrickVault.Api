using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrickVault.Api.Models;

namespace TrickVault.Api.DTOs.Trick
{
    public class CreateTrickDto
    {
        [Required]
        public required string Title { get; set; }
        public string? Effect { get; set; }
        public string? Setup { get; set; }
        public string? Method { get; set; }
        public string? Patter { get; set; }
        public string? Comments { get; set; }
        public string? Credits { get; set; }

        public List<int> CategoryIds { get; set; } = new();
    }
 }
