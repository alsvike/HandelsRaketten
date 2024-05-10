namespace HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories
{
    public class Tool : Ad
    {
        private static int _nextId = 1;
        public string Type { get; set; }
        public string Material { get; set; }
        public decimal Weight { get; set; }
        public string Manufacturer { get; set; }
        public Tool()
        {
            Active = true;
            CreatedOn = DateTime.Now;
            Id = _nextId++;
            Category = "Tool";

        }


    }
}
