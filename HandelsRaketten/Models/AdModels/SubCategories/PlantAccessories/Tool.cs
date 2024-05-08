namespace HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories
{
    public class Tool : Ad
    {
        private static int _nextId = 1;

        public Tool()
        {
            Active = true;
            CreatedOn = DateTime.Now;
            Id = _nextId++;
            Category = "Tool";

        }


    }
}
