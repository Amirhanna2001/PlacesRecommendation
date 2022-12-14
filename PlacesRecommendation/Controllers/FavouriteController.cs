using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class FavouriteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FavouriteController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var list = await _context.Favourites
                .Join(
                _context.Places,
                fav => fav.PlaceId,
                place => place.Id,

                (favourite, place) => new UserFavouritViewModel
                {
                    FavId = favourite.Id,
                    PlaceId = place.Id,
                    PlaceName = place.Name,
                    UserId = favourite.UserId,
                }).ToListAsync();
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            List<UserFavouritViewModel> userFavs = new() { };
            foreach(var favourite in list)
            {
                if (favourite.UserId.Equals(user.Id))
                    userFavs.Add(favourite);
            }
            return View(userFavs);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserFavouritViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            bool isExists = await _context.Places.AnyAsync(p => p.Name.Equals(model.PlaceName));

            if (!isExists)
            {
                ModelState.AddModelError("PlaceName", "This Place  Is Not Exists");
                return View(model);
            }

            List<UserFavouritViewModel> favs = await _context.Favourites
                .Join(
                _context.Places,
                fav => fav.PlaceId,
                place => place.Id,

                (favourite, place) => new UserFavouritViewModel
                {
                    FavId = favourite.Id,
                    PlaceId = place.Id,
                    PlaceName = place.Name,
                    UserId = favourite.UserId 
                }).ToListAsync();

            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            List<UserFavouritViewModel> userFavs = new() { };
            foreach (var favourite in favs)
            {
                if (favourite.UserId.Equals(user.Id))
                    userFavs.Add(favourite);
            }

            isExists = false;
            foreach(UserFavouritViewModel favourite in userFavs)
            {
                if (favourite.PlaceName.Equals(model.PlaceName))
                {
                    isExists = true;
                    break;
                }
            }

            Place place = _context.Places.Where(p => p.Name.Equals(model.PlaceName)).FirstOrDefault();

            if (isExists)
            {
                ModelState.AddModelError("PlaceName", "This Place  Is  Exists In Your Favourites List");
                return View(model);
            }

            
            _context.Favourites.Add(new Favourite { PlaceId = place.Id, UserId = user.Id });

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            Favourite favourite = await _context.Favourites.FindAsync(id);

            if (favourite == null) return NotFound();

            _context.Favourites.Remove(favourite);

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null) return NotFound();

        //    Favourite fav = await _context.Favourites.FindAsync(id);

        //    if (fav == null) return NotFound();
        //    IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
        //    Place place = await _context.Places.FindAsync(fav.PlaceId);
        //    UserFavouritViewModel model = new UserFavouritViewModel()
        //    {
        //        UserId = user.Id,
        //        FavId = fav.Id,
        //        PlaceId = place.Id,

        //    };

        //    return View(model);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(UserFavouritViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("Name", "Error");
        //        return View(model);
        //    }
        //    Place = await _context.Areas.FindAsync(model.Id);

        //    if (area == null) return NotFound();

        //    area.Id = model.Id;
        //    area.Name = model.Name;

        //    _context.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}

    }
}

