namespace PlacesRecommendation.Models
{
    public class Place
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public int Rate { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public Area Area { get; set; }
    }
}
