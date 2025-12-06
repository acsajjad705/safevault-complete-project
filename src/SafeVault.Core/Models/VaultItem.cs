using System.ComponentModel.DataAnnotations;

namespace SafeVault.Core.Models
{
    public class VaultItem
    {
        public Guid Id { get; set; }
        public Guid VaultId { get; set; }
        [Required, StringLength(80)] public string Name { get; set; } = "";
        [StringLength(256)] public string? Notes { get; set; }
        public string OwnerId { get; set; } = "";
    }
}
