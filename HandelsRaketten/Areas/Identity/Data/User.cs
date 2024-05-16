using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using HandelsRaketten.Models.AdModels;
using Microsoft.AspNetCore.Identity;

namespace HandelsRaketten.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    public ICollection<Ad> Ads { get; set; }
    public ICollection<Message> Messages { get; set; }
}

