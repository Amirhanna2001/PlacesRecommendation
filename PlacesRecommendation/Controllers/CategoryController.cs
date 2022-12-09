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
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //var categories = await _context.Categories.ToListAsync();
            //Dictionary<string, int> cats = new();
            //foreach (var category in categories)
            //{
            //    if(!cats.ContainsKey(category.Type))
            //        cats.Add(category.Type, category.Id);
            //}
            return View(
                await _context.Categories.ToListAsync()
                );
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            List<Category> categories = await _context.Categories.ToListAsync();

            Place place = await _context.Places.Where(a => a.Name.Equals(model.PlaceName)).FirstAsync();

            if (place == null)
            {
                ModelState.AddModelError("PlaceName", "This Place Is Not Exists");
                return View(model);
            }

            _context.Categories.Add(new Category { Type = model.CatType, PlaceId =place.Id });

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
                return BadRequest();

            Category category = await _context.Categories.FindAsync(id);
            
            if (category == null) return NotFound();

            _context.Categories.Remove(category);

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> GetAll(int? id)
        {
            if (id == null) return BadRequest();

            Category category =await _context.Categories.FindAsync(id);
            if(category == null) return NotFound();

            List<CategoryListViewModel> allPlaces =await _context.Categories
                .Join(
                    _context.Places,
                    category => category.PlaceId,
                    place => place.Id,
                    (category, place) => new CategoryListViewModel
                    {
                        PlaceId = category.PlaceId,
                        Type = category.Type,
                        PlaceName = place.Name,
                        PlaceLocation = place.Location,
                        Rate = place.Rate,
                        AreaId = place.AreaId
                    }
                ).ToListAsync();
            List<CategoryListViewModel> filteredPlaces = new() { };
            foreach (var place in allPlaces)
            {
                if (place.Type.Equals(category.Type))
                    filteredPlaces.Add(place);
            }
            return View(filteredPlaces);
        }
    }
}
