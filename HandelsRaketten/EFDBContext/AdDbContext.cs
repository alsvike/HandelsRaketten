﻿using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using Microsoft.EntityFrameworkCore;

namespace HandelsRaketten.EFDBContext
{
    public class AdDbContext : DbContext
    {
        public DbSet<IndoorPlantAd> IndoorPlant { get; set; }
        public DbSet<OutdoorPlantAd> OutdoorPlant { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=mssql2.unoeuro.com;Initial Catalog=handelsraketten_dk_db_kandis;User Id=handelsraketten_dk; Password=Drw6etgH9bfnGF3Bypza; Integrated Security=False; Connect Timeout=30; Encrypt=false");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the table name for the entity type T

            modelBuilder.Entity<Ad>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<IndoorPlantAd>("IndoorPlantAd")
            .HasValue<OutdoorPlantAd>("OutdoorPlantAd");
        }
    }
}

