using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using Microsoft.EntityFrameworkCore;

namespace HandelsRaketten.EFDBContext.AdDbContext
{
    public class IndoorPlantDbContext : DbContextGeneric<IndoorPlantAd>
    {
        public DbSet<IndoorPlantAd> IndoorPlant { get; set; }
    }
}
