namespace HandelsRaketten.Models.AdModels.SubCategories.Plants
{
    public class OutdoorPlant : Ad
    {
        private static int _nextId = 1;
        public string? Species { get; set; }
        public string? RecommendedSoil { get; set; }
        public string? FloweringSeason { get; set; }
        public string Size { get; set; }
        public OutdoorPlant()
        {
            Active = true;
            CreatedOn = DateTime.Now;
            Id = _nextId++;
            Category = "OutdoorPlant";

        }

    }
}
