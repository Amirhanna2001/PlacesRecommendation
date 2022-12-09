using System.ComponentModel.DataAnnotations;

namespace PlacesRecommendation.ViewModels
{
    public class PlaceGeneralInfo
    {
        [Required, Display(Name = "Place Id")]

        public int PlaceId { get; set; }
    }
}
