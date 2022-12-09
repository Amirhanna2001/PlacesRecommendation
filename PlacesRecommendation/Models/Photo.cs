namespace PlacesRecommendation.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public int MenuId { get; set; }
        public Place Place { get; set; }
        public Menu Menu { get; set; }
        public byte[] Image { get; set; }
    }
}
