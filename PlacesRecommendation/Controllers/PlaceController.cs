using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacesRecommendation.Data;
using PlacesRecommendation.Models;
using PlacesRecommendation.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacesRecommendation.Controllers
{
    [Authorize]
    public class PlaceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlaceController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Places
                .Join(_context.Areas,
                place=>place.AreaId,
                area=>area.Id,
                (place,area)=> new PlaceViewModel
                {
                    AreaId = area.Id,
                    PlaceName = place.Name,
                    Location = place.Location,
                    Rate = place.Rate,
                    PlaceId = place.Id,
                    AreaName = area.Name
                }
                ).ToListAsync());
        }
        public async Task<IActionResult> ViewTrend()
        {
            return View(nameof(Index),await _context.Places
                .Join(_context.Areas,
                place => place.AreaId,
                area => area.Id,
                (place, area) => new PlaceViewModel
                {
                    AreaId = area.Id,
                    PlaceName = place.Name,
                    Location = place.Location,
                    Rate = place.Rate,
                    PlaceId = place.Id,
                    AreaName = area.Name
                }
                ).ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddPlaceViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            List<Place> places = await _context.Places.ToListAsync();

            bool isExists = await _context.Places.AnyAsync(p => p.Name.Equals(model.Name));

            if (isExists)
            {
                ModelState.AddModelError("Name", "This Place Is Already Exists");
                return View(model);
            }
             isExists = await _context.Areas.AnyAsync(a => a.Id == model.AreaId);


            if (!isExists)
            {
                ModelState.AddModelError("AreaId", "This Area Id  Is Not Exists");
                return View(model);
            }
            _context.Places.Add(new Place {
                Name = model.Name , 
                AreaId = model.AreaId, 
                Location = model.Location,
                Rate = model.Rate});

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            Place place = await _context.Places.FindAsync(id);

            if (place == null) return NotFound();

            _context.Places.Remove(place);

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Search(SearchViewModel search)
        {
           return View(nameof(Index),await _context.Places
                .Where(p => p.Name.Contains(search.Item))
                .Join(_context.Areas,
                place => place.AreaId,
                area => area.Id,
                (place, area) => new PlaceViewModel
                {
                    AreaId = area.Id,
                    PlaceName = place.Name,
                    Location = place.Location,
                    Rate = place.Rate,
                    PlaceId = place.Id,
                    AreaName = area.Name
                }
                ).ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            Place place = await _context.Places.FindAsync(id);

            if (place == null) return NotFound();
            AddPlaceViewModel model = new()
            {
                AreaId = place.AreaId,
                Name = place.Name,
                Rate = place.Rate,
                Location = place.Location,
                Id = place.Id,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AddPlaceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Name", "Error");
                return View(model);
            }
            Place place =await  _context.Places.FindAsync(model.Id);

            if (place == null) return NotFound();

             //_context.Places.Remove(place);

            place.Name = model.Name;
            place.Rate = model.Rate;
            place.AreaId = model.AreaId;
            place.Location = model.Location;

            //await _context.Places.AddAsync(place);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
