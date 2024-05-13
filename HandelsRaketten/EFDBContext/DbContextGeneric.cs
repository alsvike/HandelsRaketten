using HandelsRaketten.Models.AdModels;
using Microsoft.EntityFrameworkCore;

namespace HandelsRaketten.EFDBContext
{
    public class DbContextGeneric<T> : DbContext where T : class
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=mssql2.unoeuro.com;Initial Catalog=handelsraketten_dk_db_kandis;User Id=handelsraketten_dk; Password=Drw6etgH9bfnGF3Bypza; Integrated Security=False; Connect Timeout=30; Encrypt=false");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the table name for the entity type T
            modelBuilder.Entity<T>().ToTable(typeof(T).Name);

            // Check if T is assignable to Ad
            if (typeof(T).IsSubclassOf(typeof(Ad)))
            {
                modelBuilder.Entity<T>()
                    .Property("Id")
                    .ValueGeneratedOnAdd();
            }
        }

        public DbSet<T> Obj { get; set; }
    }

}
