namespace HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories
{
    public class GardeningTool : Ad
    {
        //private static int _nextId = 1;
        public string? Type { get; set; }
        public string? Material { get; set; }
        public decimal? Weight { get; set; }
        public string? Manufacturer { get; set; }
        public bool? IsElectric { get; set; }
        public bool? IsManual { get; set; }
        public bool? IsHandheld { get; set; }
        public bool? IsHeavyDuty { get; set; }

        public GardeningTool()
        {
            Active = true;
            CreatedOn = DateTime.Now;
            //Id = _nextId++;
            Category = "GardeningTool";

        }




    }
}
