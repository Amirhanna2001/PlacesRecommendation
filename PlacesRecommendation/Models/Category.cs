namespace PlacesRecommendation.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; }

    }
}
