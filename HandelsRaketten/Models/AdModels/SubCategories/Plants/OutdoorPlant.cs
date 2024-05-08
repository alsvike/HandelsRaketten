namespace HandelsRaketten.Models.AdModels.SubCategories.Plants
{
    public class OutdoorPlant : Ad
    {
        private static int _nextId = 1;

        public OutdoorPlant()
        {
            Active = true;
            CreatedOn = DateTime.Now;
            Id = _nextId++;
            Category = "OutdoorPlant";

        }

    }
}
