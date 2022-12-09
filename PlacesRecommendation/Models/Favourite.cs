using Microsoft.AspNetCore.Identity;

namespace PlacesRecommendation.Models
{
    public class Favourite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PlaceId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public Place Place { get; set; }
    }
}
