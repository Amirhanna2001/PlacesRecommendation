using System.ComponentModel.DataAnnotations;

namespace PlacesRecommendation.ViewModels
{
    public class AddPlaceViewModel
    {
        [Required]
        [Display(Name = "Area Id")]
        public int AreaId { get; set; }
        [Required]
        [Range(0,10)]
        public int Rate { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public int Id { get; set; }
    }
}
