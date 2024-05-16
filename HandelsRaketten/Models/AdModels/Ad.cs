﻿using HandelsRaketten.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandelsRaketten.Models.AdModels
{
    public class Ad
    {
        [Key]
        public virtual int Id { get; set; }
        [Required]
        public string Category { get; set; }
        public string? Discriminator { get; set; }
        [StringLength(50, ErrorMessage = "Navn må ikke være over 50 tegn")]
        public string? Title { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOn { get; set; }

        [StringLength(2000, ErrorMessage = "Beskrivelse må ikke være over 2000 tegn")]
        public string? Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Pris kan ikke være et negativt tal")]
        public int Price { get; set; }

        public string? AdImage { get; set; }
        public User? Owner { get; set; }
        public string? UserId { get; set; }
        public Seller? Seller { get; set; }
        public int SellerId { get; set; }
        public ICollection<Message>? Messages { get; set; }


    }
}
