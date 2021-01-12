using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _hostEnvironment;


        public ArtPiecesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
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
        public async Task<IActionResult> Create([Bind("ArtPieceId,Author,Info,ModifiedDate,AuthorsNote,TypeOfArt,Style,Reserved,PictureFile,Price,Horizontal")] ArtPiece artPiece)
        {
            if (ModelState.IsValid)
            {
                // saving pictures of art pieces to wwwroot/StoredPictures 
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(artPiece.PictureFile.FileName);
                string extension = Path.GetExtension(artPiece.PictureFile.FileName);
                artPiece.PicUrl = fileName = fileName + DateTime.Now.ToString("yymmss") + extension;
                string path = Path.Combine(wwwRootPath + "/StoredPictures/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await artPiece.PictureFile.CopyToAsync(fileStream);
                }
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
        public async Task<IActionResult> Edit(int id, [Bind("ArtPieceId,Author,Info,ModifiedDate,AuthorsNote,TypeOfArt,Style,Reserved,PicUrl,Horizontal,Price")] ArtPiece artPiece)
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
