namespace HandelsRaketten.Models.AdModels.SubCategories.Plants
{
    public class IndoorPlant : Ad
    {
        private static int _nextId = 1;

        public string? UniqueProperty { get; set; }
        public IndoorPlant()
        {
            Active = true;
            CreatedOn = DateTime.Now;
            Id = _nextId++;
            Category = "IndoorPlant";

        }

    }
}
