﻿using HandelsRaketten.Areas.Identity.Data;
using HandelsRaketten.Data;
using HandelsRaketten.EFDBContext;
using HandelsRaketten.Models;
using HandelsRaketten.Models.AdModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HandelsRaketten.Services.DbServices
{
    public class AdDbService : IAdDbService
    {
        private readonly IConfiguration _configuration;



        public AdDbService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task AddObjectAsync(Ad obj)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<HandelsRakettenContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new HandelsRakettenContext(optionsBuilder.Options))
            {
                context.Set<Ad>().Add(obj);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Ad>> GetObjectsAsync()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<HandelsRakettenContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new HandelsRakettenContext(optionsBuilder.Options))
            { 
                return await context.Set<Ad>().AsNoTracking().ToListAsync();
            }
        }

        public async Task SaveObjects(List<Ad> objs)
        {
            using (var context = new DbContextGeneric<Ad>())
            {
                foreach (Ad obj in objs)
                {
                    context.Set<Ad>().Add(obj);
                    await context.SaveChangesAsync();
                }

                context.SaveChanges();
            }
        }

        public async Task UpdateObjectAsync(Ad obj)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<HandelsRakettenContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new HandelsRakettenContext(optionsBuilder.Options))
            {
                context.Set<Ad>().Update(obj);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteObjectAsync(Ad obj)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<HandelsRakettenContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new HandelsRakettenContext(optionsBuilder.Options))
            {
                context.Set<Ad>().Remove(obj);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Ad> GetObjectByIdAsync(int id)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<HandelsRakettenContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new HandelsRakettenContext(optionsBuilder.Options))
            {
                return context.Set<Ad>().Find(id);

            }
        }


        public async Task<Ad> GetAdConversationAsync(int adId, string category)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<HandelsRakettenContext>();
            optionsBuilder.UseSqlServer(connectionString);

            Ad ad;
            using (var context = new HandelsRakettenContext(optionsBuilder.Options))
            {
                 ad = await context.Set<Ad>()
                    .Include(a => a.Owner)
                    .Include(a => a.Messages)
                    .ThenInclude(m => m.Sender)
                    .FirstOrDefaultAsync(m => m.Id == adId && m.Category == category);

            }
            return ad;
        }
    }
}
