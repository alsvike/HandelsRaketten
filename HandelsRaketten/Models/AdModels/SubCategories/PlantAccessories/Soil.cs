namespace HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories
{
    public class Soil : Ad
    {
        //private static int _nextId = 1;
        public string? Type { get; set; }
        public decimal? pH { get; set; }
        public decimal? MoistureLevel { get; set; }
        public decimal? NutrientLevel { get; set; }
        public string? Texture { get; set; }
        public string? Color { get; set; }
        public Soil()
        {
            Active = true;
            CreatedOn = DateTime.Now;
            //Id = _nextId++;
            Category = "Soil";

        }



    }
}
