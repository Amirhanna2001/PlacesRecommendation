using System.ComponentModel.DataAnnotations;

namespace PlacesRecommendation.ViewModels
{
    public class UserFavouritViewModel
    {
        public string UserId  { get; set; }
        public int FavId { get; set; }
        public int PlaceId { get; set; }
        [Required,Display(Name ="Place Name")]
        public string PlaceName { get; set; }
    }
}
