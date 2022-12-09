namespace PlacesRecommendation.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
