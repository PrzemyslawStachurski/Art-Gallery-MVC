using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class ArtPiecesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtPiecesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArtPieces
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArtPiece.ToListAsync());
        }

        // GET: ArtPieces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artPiece = await _context.ArtPiece
                .FirstOrDefaultAsync(m => m.ArtPieceId == id);
            if (artPiece == null)
            {
                return NotFound();
            }

            return View(artPiece);
        }

        // GET: ArtPieces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArtPieces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtPieceId,Author,Info,ModifiedDate,AuthorsNote,TypeOfArt,Style,Reserved,PicUrl,Horizontal")] ArtPiece artPiece)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artPiece);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artPiece);
        }

        // GET: ArtPieces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artPiece = await _context.ArtPiece.FindAsync(id);
            if (artPiece == null)
            {
                return NotFound();
            }
            return View(artPiece);
        }

        // POST: ArtPieces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtPieceId,Author,Info,ModifiedDate,AuthorsNote,TypeOfArt,Style,Reserved,PicUrl,Horizontal")] ArtPiece artPiece)
        {
            if (id != artPiece.ArtPieceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artPiece);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtPieceExists(artPiece.ArtPieceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artPiece);
        }

        // GET: ArtPieces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artPiece = await _context.ArtPiece
                .FirstOrDefaultAsync(m => m.ArtPieceId == id);
            if (artPiece == null)
            {
                return NotFound();
            }

            return View(artPiece);
        }

        // POST: ArtPieces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artPiece = await _context.ArtPiece.FindAsync(id);
            _context.ArtPiece.Remove(artPiece);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtPieceExists(int id)
        {
            return _context.ArtPiece.Any(e => e.ArtPieceId == id);
        }
    }
}
