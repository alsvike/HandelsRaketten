using System.ComponentModel.DataAnnotations;

namespace HandelsRaketten.Models.AdModels
{
    public class Ad
    {
        [Key]
        public virtual int Id { get; set; }
        public string? Category { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string AdImage { get; set; }

    }
}
