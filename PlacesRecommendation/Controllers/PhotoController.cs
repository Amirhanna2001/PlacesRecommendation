using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacesRecommendation.Data;
using PlacesRecommendation.Models;
using PlacesRecommendation.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PlacesRecommendation.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        private ApplicationDbContext _context;

        public PhotoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Photos.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PhotoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //List<Photo> photos = await _context.Photos.ToListAsync();

            bool isExists = await _context.Photos.AnyAsync(a => a.MenuId == model.MenuId);

            if (!isExists)
            {
                ModelState.AddModelError("MenuId", "This Menu Is Not Exists");
                return View(model);
            }
            isExists = await _context.Photos.AnyAsync(a => a.PlaceId == model.PlaceId);

            if (!isExists)
            {
                ModelState.AddModelError("PlaceId", "This Place Is Not Exists");
                return View(model);
            }
            if (!Request.Form.Files.Any())
            {
                ModelState.AddModelError("Image", "Please select movie poster!");
                return View( model);
            }
            IFormFile poster = Request.Form.Files.FirstOrDefault();

            using var dataStream = new MemoryStream();
            await poster.CopyToAsync(dataStream);
            _context.Photos.Add(
                new Photo{
                    PlaceId = model.PlaceId,
                    MenuId = model.MenuId ,
                    Image = dataStream.ToArray()
            });

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            Category category = await _context.Categories.FindAsync(id);

            if (category == null) return NotFound();

            _context.Categories.Remove(category);

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
    }
}
