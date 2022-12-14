using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacesRecommendation.Data;
using PlacesRecommendation.Models;
using PlacesRecommendation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlacesRecommendation.Controllers
{
    [Authorize]
    public class AreaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AreaController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Areas.ToListAsync());
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        
        public async Task< IActionResult> Create(AddAreaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            List<Area> areas = await _context.Areas.ToListAsync();

            bool isExists = await _context.Areas.AnyAsync(a => a.Name.Equals( model.Name));

            if (isExists) {
                ModelState.AddModelError("Name", "This Area Is Already Exists");
                return View(model);
            }

            _context.Areas.Add(new Area { Name = model.Name });

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            Area area = await _context.Areas.FindAsync(id);

            if (area == null) return NotFound();

            _context.Areas.Remove(area);

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            Area area =await _context.Areas.FindAsync(id);

            if (area == null) return NotFound();
            AddAreaViewModel model = new()
            {
                Id = area.Id,
                Name = area.Name
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AddAreaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Name","Error");
                return View(model);
            }
            Area area =await _context.Areas.FindAsync(model.Id);

            if (area == null) return NotFound();

            area.Id = model.Id;
            area.Name = model.Name;
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }

   
}
