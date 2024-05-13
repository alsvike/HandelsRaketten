using System.ComponentModel.DataAnnotations;

namespace HandelsRaketten.Models
{
    public class Seller
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public Seller()
        {
        }
    }
}
