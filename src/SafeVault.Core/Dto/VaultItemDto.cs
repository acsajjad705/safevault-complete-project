using System.ComponentModel.DataAnnotations;

namespace SafeVault.Core.Dto
{
    public class VaultItemDto
    {
        [Required, StringLength(80)] public string Name { get; set; } = "";
        [StringLength(256)] public string? Notes { get; set; }
    }
}
