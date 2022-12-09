using System.ComponentModel.DataAnnotations;

namespace PlacesRecommendation.ViewModels
{
    public class PhotoViewModel:PlaceGeneralInfo
    {
        [Required, Display(Name = "Menu Id")]
        public int MenuId { get; set; }
        [Required]
        public byte[] Image { get; set; }
    }
}
