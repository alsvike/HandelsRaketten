using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using Microsoft.EntityFrameworkCore;

namespace HandelsRaketten.EFDBContext.AdDbContext
{
    public class IndoorPlantDbContext : DbContextGeneric<IndoorPlant>
    {
        public DbSet<IndoorPlant> IndoorPlant { get; set; }
    }
}
