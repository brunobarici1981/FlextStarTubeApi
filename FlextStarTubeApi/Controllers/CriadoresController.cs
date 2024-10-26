
using Microsoft.AspNetCore.Mvc;
using FlextStarTubeApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FlextStarTubeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriadoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CriadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Criador>>> GetCriadores()
        {
            return await _context.Criadores.Include(c => c.Usuario).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Criador>> GetCriador(int id)
        {
            var criador = await _context.Criadores
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (criador == null)
            {
                return NotFound();
            }
            return criador;
        }

        [HttpPost]
        public async Task<ActionResult<Criador>> PostCriador(Criador criador)
        {
            _context.Criadores.Add(criador);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCriador), new { id = criador.Id }, criador);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCriador(int id, Criador criador)
        {
            if (id != criador.Id)
            {
                return BadRequest();
            }

            _context.Entry(criador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CriadorExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCriador(int id)
        {
            var criador = await _context.Criadores.FindAsync(id);
            if (criador == null)
            {
                return NotFound();
            }

            _context.Criadores.Remove(criador);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CriadorExists(int id)
        {
            return _context.Criadores.Any(e => e.Id == id);
        }
    }
}
