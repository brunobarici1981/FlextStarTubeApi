using Microsoft.AspNetCore.Mvc;
using FlextStarTubeApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FlextStarTubeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPlaylistsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemPlaylistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemPlaylist>>> GetItemPlaylists()
        {
            return await _context.ItemPlaylists.Include(ip => ip.Playlist).Include(ip => ip.Conteudo).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemPlaylist>> GetItemPlaylist(int id)
        {
            var itemPlaylist = await _context.ItemPlaylists
                .Include(ip => ip.Playlist)
                .Include(ip => ip.Conteudo)
                .FirstOrDefaultAsync(ip => ip.Id == id); // Corrigido aqui
            if (itemPlaylist == null)
            {
                return NotFound();
            }
            return itemPlaylist;
        }

        [HttpPost]
        public async Task<ActionResult<ItemPlaylist>> PostItemPlaylist(ItemPlaylist itemPlaylist)
        {
            _context.ItemPlaylists.Add(itemPlaylist);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItemPlaylist), new { id = itemPlaylist.Id }, itemPlaylist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemPlaylist(int id, ItemPlaylist itemPlaylist)
        {
            if (id != itemPlaylist.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemPlaylist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemPlaylistExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemPlaylist(int id)
        {
            var itemPlaylist = await _context.ItemPlaylists.FindAsync(id);
            if (itemPlaylist == null)
            {
                return NotFound();
            }

            _context.ItemPlaylists.Remove(itemPlaylist);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ItemPlaylistExists(int id)
        {
            return _context.ItemPlaylists.Any(e => e.Id == id);
        }

    }
}
