using Microsoft.AspNetCore.Identity;

namespace PlacesRecommendation.Models
{
    public class UserType
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public IdentityUser User { get; set; }
    }
}
