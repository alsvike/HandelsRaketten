using HandelsRaketten.Areas.Identity.Data;
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HandelsRaketten.Data;

public class HandelsRakettenContext : IdentityDbContext<User>
{
    public DbSet<IndoorPlantAd> IndoorPlantAd { get; set; }
    public DbSet<OutdoorPlantAd> OutdoorPlantAd { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<User> user { get; set; }
    public HandelsRakettenContext(DbContextOptions<HandelsRakettenContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<User>();

        builder.Entity<Ad>()
    .HasDiscriminator<string>("Discriminator")
    .HasValue<IndoorPlantAd>("IndoorPlantAd")
    .HasValue<OutdoorPlantAd>("OutdoorPlantAd");
    }

}
