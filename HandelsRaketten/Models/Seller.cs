using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HandelsRaketten.Models
{
    public class Seller
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Email skal udfyldes")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "By skal udfyldes")]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Zip kode skal være på 4 cifre")]
        public int ZipCode { get; set; }

        [StringLength(50)]
        public string? Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{8}|(\d{2}\s){3}\d{2})$", ErrorMessage = "Nummer findes ikke")]
        public string? Phone { get; set; }

        public Seller()
        {
        }
    }
}
