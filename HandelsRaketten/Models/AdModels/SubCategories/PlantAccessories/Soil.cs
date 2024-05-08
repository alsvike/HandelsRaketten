namespace HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories
{
    public class Soil : Ad
    {
        private static int _nextId = 1;

        public Soil()
        {
            Active = true;
            CreatedOn = DateTime.Now;
            Id = _nextId++;
            Category = "Soil";

        }



    }
}
