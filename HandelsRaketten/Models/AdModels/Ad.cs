namespace HandelsRaketten.Models.AdModels
{
    public class Ad
    {
        public int? Id { get; set; }
        public string? Category { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string AdImage { get; set; }

        public Ad(string name, string description, int price, string adImage)
        {
            Name = name;
            Active = true;
            CreatedOn = DateTime.Now;
            Description = description;
            Price = price;
            AdImage = adImage;
        }

        public Ad()
        {
            CreatedOn = DateTime.Now;
            Active = true;
        }
    }
}
