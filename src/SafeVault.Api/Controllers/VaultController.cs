using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeVault.Core.Dto;
using SafeVault.Core.Models;
using SafeVault.Data;

namespace SafeVault.Api.Controllers
{
    [ApiController]
    [Route("api/vaults")]
    public class VaultController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _http;

        public VaultController(AppDbContext db, IHttpContextAccessor http)
        {
            _db = db;
            _http = http;
        }

        [Authorize(Policy = "CanManageVault")]
        [HttpPost("{id:guid}/items")]
        public async Task<IActionResult> AddItem(Guid id, [FromBody] VaultItemDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var ownerId = _http.HttpContext!.User.FindFirst("sub")?.Value ?? "";
            var item = new VaultItem { VaultId = id, Name = dto.Name, Notes = dto.Notes, OwnerId = ownerId };

            _db.VaultItems.Add(item);
            await _db.SaveChangesAsync();
            return Ok(item);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{ownerId}/items")]
        public async Task<IActionResult> GetItemsForOwner(string ownerId)
        {
            // Parameterized query via LINQ prevents SQL injection
            var items = await _db.VaultItems.Where(v => v.OwnerId == ownerId).ToListAsync();
            return Ok(items);
        }
    }
}
