using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MVC.Data;
using MVC.Models;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class ArtPiecesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IStringLocalizer<ArtPiecesController> _localizer;

        [TempData]
        public string Message { get; set; }
        [TempData]
        public string ArtAction { get; set; }



        public ArtPiecesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, IStringLocalizer<ArtPiecesController> localizer)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
            this._localizer = localizer;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder, string searchString, string author, string type, string price_min, string price_max)
        {
            ViewData["AuthorSortParm"] = String.IsNullOrEmpty(sortOrder) ? "author" : "";
            ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date" : "Date";


            var artPieces = from s in _context.ArtPiece select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                artPieces = artPieces.Where(s => s.Author.Contains(searchString) ||
                                        s.Info.Contains(searchString) || s.TypeOfArt.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(author))
            {
                artPieces = artPieces.Where(s => s.Author.Contains(author));

            }
            if (!String.IsNullOrEmpty(type))
            {
                artPieces = artPieces.Where(s => s.TypeOfArt.Contains(type));

            }

            //BTC API
            BitcoinApi bitcoin;

            using (var data = new WebClient())
            {
                try
                {
                    string response = data.DownloadString("https://bitbay.net/API/Public/btc/ticker.json");
                    bitcoin = JsonConvert.DeserializeObject<BitcoinApi>(response);
                    var price = bitcoin.Average;
                    ViewBag.BitcoinPrice = price;
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentNullException || ex is WebException)
                    {
                        ViewBag.BitcoinPrice = 0;
                    }
                }
            }


            switch (sortOrder)
            {
                case "author":
                    artPieces = artPieces.OrderBy(s => s.Author);
                    break;
                case "price":
                    artPieces = artPieces.OrderByDescending(s => s.Price);
                    break;
                case "Date":
                    artPieces = artPieces.OrderBy(s => s.ModifiedDate);
                    break;
                default:
                    artPieces = artPieces.OrderByDescending(s => s.ModifiedDate);
                    break;
            }



            return View(await artPieces.AsNoTracking().ToListAsync());

        }
        //Details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            BitcoinApi bitcoin;

            using (var data = new WebClient())
            {
                try
                {
                    string response = data.DownloadString("https://bitbay.net/API/Public/btc/ticker.json");
                    bitcoin = JsonConvert.DeserializeObject<BitcoinApi>(response);
                    var price = bitcoin.Average;
                    ViewBag.BitcoinPrice = price;
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentNullException || ex is WebException)
                    {
                        ViewBag.BitcoinPrice = 0;
                    }
                }
            }
            if (id == null)
            {
                return NotFound();
            }


            try
            {
                var artPiece = await _context.ArtPiece
                .FirstOrDefaultAsync(m => m.ArtPieceId == id);
                if (artPiece == null)
                {
                    return NotFound();
                }
                return View(artPiece);
            }
            catch (DbUpdateException)
            {
                return View("Error");
            }
        }

        //Get - Create
        public IActionResult Create()
        {
            return View();
        }

        //Post - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtPieceId,Author,Info,ModifiedDate,AuthorsNote,TypeOfArt,Style,Reserved,PictureFile,Price,Horizontal")] ArtPiece artPiece)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(artPiece.PictureFile.FileName);
                string extension = Path.GetExtension(artPiece.PictureFile.FileName);
                artPiece.PicUrl = fileName = fileName + DateTime.Now.ToString("yymmss") + extension;
                string path = Path.Combine(wwwRootPath + "/StoredPictures/", fileName);

                try
                {
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await artPiece.PictureFile.CopyToAsync(fileStream);
                    }
                    _context.Add(artPiece);
                    await _context.SaveChangesAsync();
                    Message = $"Art {artPiece.Info} added";
                    ArtAction = "Create";
                }
                catch (DbUpdateException)
                {
                    return View("Error");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(artPiece);
        }

        //Get - Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var artPiece = await _context.ArtPiece.FindAsync(id);

                if (artPiece == null)
                {
                    return NotFound();
                }
                return View(artPiece);
            }
            catch
            {
                return View("Error");
            }

        }

        //Post - Edit
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
                    Message = $"Art {artPiece.Info} edited";
                    ArtAction = "Edit";
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

        // Get - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var artPiece = await _context.ArtPiece
                .FirstOrDefaultAsync(m => m.ArtPieceId == id);
                if (artPiece == null)
                {
                    return NotFound();
                }

                return View(artPiece);
            }
            catch
            {
                return View("Error");
            }

        }

        // Post - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var artPiece = await _context.ArtPiece.FindAsync(id);
                _context.ArtPiece.Remove(artPiece);
                await _context.SaveChangesAsync();
                Message = $"Art {artPiece.Info} deleted";
                ArtAction = "Delete";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return View("Error");
            }

        }

        private bool ArtPieceExists(int id)
        {
            return _context.ArtPiece.Any(e => e.ArtPieceId == id);
        }
    }
}