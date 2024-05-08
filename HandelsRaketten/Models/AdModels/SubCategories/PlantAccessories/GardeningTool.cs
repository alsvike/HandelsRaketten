namespace HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories
{
    public class GardeningTool : Ad
    {
        private static int _nextId = 1;

        public GardeningTool()
        {
            Active = true;
            CreatedOn = DateTime.Now;
            Id = _nextId++;
            Category = "GardeningTool";

        }




    }
}
