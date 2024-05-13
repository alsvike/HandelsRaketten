namespace HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories
{
    public class Fertilizer : Ad
    {
        //private static int _nextId = 1;

        public string? Type { get; set; }
        public decimal? NPKRatio_N { get; set; }
        public decimal? NPKRatio_P { get; set; }
        public decimal? NPKRatio_K { get; set; }
        public string? Manufacturer { get; set; }
        public string? UsageInstructions { get; set; }
        public Fertilizer()
        {
            Active = true;
            CreatedOn = DateTime.Now;
            //Id = _nextId++;
            Category = "Fertilizer";
        }

    }
}
