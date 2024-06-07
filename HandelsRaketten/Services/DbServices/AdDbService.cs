using HandelsRaketten.Areas.Identity.Data;
using HandelsRaketten.Data;
using HandelsRaketten.EFDBContext;
using HandelsRaketten.Models;
using HandelsRaketten.Models.AdModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Protocol;

namespace HandelsRaketten.Services.DbServices
{
    public class AdDbService : IAdDbService
    {


        public async Task AddObjectAsync(Ad obj)
        {


            using (var context = new AdDbContext())
            {
                context.Set<Ad>().Add(obj);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddMessageAsync(Message message)
        {

            using (var context = new AdDbContext())
            {
                context.Messages.Add(message);
                await context.SaveChangesAsync();
            }
            
        }

        public async Task<List<Ad>> GetAllByUserIdAsync(string userId)
        {
            List<Ad> _ads;

            using (var context = new AdDbContext())
            {
                _ads = await context.Set<Ad>()
                    .Include(o => o.Owner)
                    .Where(a => a.Owner.Id == userId)
                    .ToListAsync();
            }
            return _ads;
        }


        public async Task<IEnumerable<Ad>> GetObjectsAsync()
        {
            using (var context = new AdDbContext())
            { 
                return await context.Set<Ad>().AsNoTracking().ToListAsync();
            }
        }

        public async Task SaveObjectsAsync(List<Ad> objs)
        {
            using (var context = new AdDbContext())
            {
                foreach (Ad obj in objs)
                {
                    context.Set<Ad>().Add(obj);
                    await context.SaveChangesAsync();
                }

                context.SaveChanges();
            }
        }

        public async Task DeleteMessageAsync(int msgId)
        {
            Message msg;
            using (var context = new AdDbContext())
            {
                msg = context.Messages.Find(msgId);

                if (msg == null) return;

                context.Messages.Remove(msg);
                await context.SaveChangesAsync();
            }
        }


        public async Task UpdateObjectAsync(Ad obj)
        {
            using (var context = new AdDbContext())
            {
                context.Set<Ad>().Update(obj);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteObjectAsync(Ad obj)
        {
            using (var context = new AdDbContext())
            {
                context.Set<Ad>().Remove(obj);
                await context.SaveChangesAsync();
            }
        }


        public async Task<Ad> GetObjectByIdAsync(int id)
        {
            using (var context = new AdDbContext())
            {
                return context.Set<Ad>().Find(id);

            }
        }

        public async Task<Ad> GetAdConversationAsync(int adId)
        {


            Ad ad;
            using (var context = new AdDbContext())
            {
                ad = await context.Set<Ad>()
                   .Include(o => o.Owner)
                   .Include(a => a.AdConversations)
                        .ThenInclude(m => m.Messages)
                            .ThenInclude(s => s.Sender)
                   .FirstOrDefaultAsync(m => m.Id == adId);

            }
            return ad;
        }

        //Skal have kategori som parameter - Viser alle planter
        public async Task<List<Ad>> GetAllByCategoryAsync(string category)
        {

            List<Ad> ads;
            using (var context = new AdDbContext())
            {
                ads = await context.Set<Ad>()
                        .Where(m => m.Category == category)
                        .ToListAsync();

                return ads;
            }

        }

        //Viser underkategorier såsom (Udendørsplanter/Indendørsplanter)
        public async Task<List<Ad>> GetAllBySubcategoryAsync(string discriminator)
        {
            List<Ad> ads;
            using (var context = new AdDbContext())
            {
                ads = await context.Set<Ad>()
                    .Where(ad => EF.Property<string>(ad, "Discriminator") == discriminator)
                    .ToListAsync();
                return ads;
            }
        }
    }
}
