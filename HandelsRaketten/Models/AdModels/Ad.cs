using HandelsRaketten.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandelsRaketten.Models.AdModels
{
    public abstract class Ad
    {
        [Key]
        public virtual int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string AdImage { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Seller Seller { get; set; }
        public int SellerId { get; set; }   

    }   
}
