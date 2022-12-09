using System.ComponentModel.DataAnnotations;

namespace PlacesRecommendation.ViewModels
{
    public class CategoryViewModel
    {
        public int CatId { get; set; }
        [Required,Display(Name ="Type")]
        public string CatType { get; set; }
        [Required, Display(Name = "Place Name")]
        public string PlaceName { get; set; }
        public int PlaceId { get; set; }
    }
}
