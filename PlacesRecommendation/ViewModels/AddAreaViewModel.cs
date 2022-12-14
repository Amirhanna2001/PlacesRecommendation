using System.ComponentModel.DataAnnotations;

namespace PlacesRecommendation.ViewModels
{
    public class AddAreaViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
