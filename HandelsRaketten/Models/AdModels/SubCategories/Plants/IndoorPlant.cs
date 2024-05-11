namespace HandelsRaketten.Models.AdModels.SubCategories.Plants
{
    public class IndoorPlant : Ad
    {
        //private static int _nextId = 1;

        public string? Species { get; set; }
        public string? RecommendedTemperature { get; set; }
        public string? WateringNeeds { get; set; }
        public string? SunlightNeeds { get; set; }
        public string? RecommendedSoil { get; set; }
        public string? Size { get; set; }
        public string? PotSize { get; set; }
        public IndoorPlant()
        {
            Active = true;
            CreatedOn = DateTime.Now;
            //Id = _nextId++;
            Category = "IndoorPlant";

        }

    }
}
