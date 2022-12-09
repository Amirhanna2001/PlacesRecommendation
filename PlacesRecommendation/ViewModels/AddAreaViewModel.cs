using System.ComponentModel.DataAnnotations;

namespace PlacesRecommendation.ViewModels
{
    public class AddAreaViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
