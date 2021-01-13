using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ArtPiecesApi : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArtPiecesApi(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ArtPiecesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtPiece>>> GetArtPiece()
        {
            return await _context.ArtPiece.ToListAsync();
        }

        // GET: api/ArtPiecesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtPiece>> GetArtPiece(int id)
        {
            var artPiece = await _context.ArtPiece.FindAsync(id);

            if (artPiece == null)
            {
                return NotFound();
            }

            return artPiece;
        }

        // PUT: api/ArtPiecesApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtPiece(int id, ArtPiece artPiece)
        {
            if (id != artPiece.ArtPieceId)
            {
                return BadRequest();
            }

            _context.Entry(artPiece).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtPieceExists(id))
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

        // POST: api/ArtPiecesApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ArtPiece>> PostArtPiece(ArtPiece artPiece)
        {
            _context.ArtPiece.Add(artPiece);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtPiece", new { id = artPiece.ArtPieceId }, artPiece);
        }

        // DELETE: api/ArtPiecesApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ArtPiece>> DeleteArtPiece(int id)
        {
            var artPiece = await _context.ArtPiece.FindAsync(id);
            if (artPiece == null)
            {
                return NotFound();
            }

            _context.ArtPiece.Remove(artPiece);
            await _context.SaveChangesAsync();

            return artPiece;
        }

        private bool ArtPieceExists(int id)
        {
            return _context.ArtPiece.Any(e => e.ArtPieceId == id);
        }
    }
}
