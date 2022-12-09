using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacesRecommendation.Data;
using PlacesRecommendation.Models;
using PlacesRecommendation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlacesRecommendation.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext _context;

        public MenuController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //menu and place
            return View(await _context.Menus.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MenuViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            List<Menu> areas = await _context.Menus.ToListAsync();

            bool isExists = await _context.Places.AnyAsync(a => a.Id == model.PlaceId);

            if (!isExists)
            {
                ModelState.AddModelError("PlaceId", "This Place  Is Not Exists");
                return View(model);
            }

            _context.Menus.Add(new Menu { PlaceId = model.PlaceId });

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            Menu menu = await _context.Menus.FindAsync(id);

            if (menu == null) return NotFound();

            _context.Menus.Remove(menu);

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
    }
}
