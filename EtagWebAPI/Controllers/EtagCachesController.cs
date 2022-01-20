#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EtagWebAPI.Models;

namespace EtagWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtagCachesController : ControllerBase
    {
        private readonly EtagContext _context;

        public EtagCachesController(EtagContext context)
        {
            _context = context;
        }

        // GET: api/EtagCaches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EtagCache>>> GetEtagItems()
        {
            return await _context.EtagItems.ToListAsync();
        }

        // GET: api/EtagCaches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EtagCache>> GetEtagCache(long id)
        {
            var etagCache = await _context.EtagItems.FindAsync(id);

            if (etagCache == null)
            {
                return NotFound();
            }

            return etagCache;
        }

        // PUT: api/EtagCaches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEtagCache(long id, EtagCache etagCache)
        {
            if (id != etagCache.Id)
            {
                return BadRequest();
            }

            _context.Entry(etagCache).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtagCacheExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EtagCaches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EtagCache>> PostEtagCache(EtagCache etagCache)
        {
            _context.EtagItems.Add(etagCache);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEtagCache", new { id = etagCache.Id }, etagCache);
        }

        // DELETE: api/EtagCaches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtagCache(long id)
        {
            var etagCache = await _context.EtagItems.FindAsync(id);
            if (etagCache == null)
            {
                return NotFound();
            }

            _context.EtagItems.Remove(etagCache);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EtagCacheExists(long id)
        {
            return _context.EtagItems.Any(e => e.Id == id);
        }
    }
}
