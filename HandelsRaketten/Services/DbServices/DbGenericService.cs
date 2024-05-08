using HandelsRaketten.EFDBContext;
using Microsoft.EntityFrameworkCore;

namespace HandelsRaketten.Services.DbServices
{
    public class DbGenericService<T> : IService<T> where T : class
    {
        public async Task AddObjectAsync(T obj)
        {
            using (var context = new DbContextGeneric<T>())
            {
                context.Set<T>().Add(obj);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetObjectsAsync()
        {
            using (var context = new DbContextGeneric<T>())
            {
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task SaveObjects(List<T> objs)
        {
            using (var context = new DbContextGeneric<T>())
            {
                foreach (T obj in objs)
                {
                    context.Set<T>().Add(obj);
                    await context.SaveChangesAsync();
                }

                context.SaveChanges();
            }
        }

        public async Task UpdateObjectAsync(T obj)
        {
            using (var context = new DbContextGeneric<T>())
            {
                context.Set<T>().Update(obj);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteObjectAsync(T obj)
        {
            using (var context = new DbContextGeneric<T>())
            {
                context.Set<T>().Remove(obj);
                await context.SaveChangesAsync();
            }
        }

        public async Task<T> GetObjectByIdAsync(int id)
        {
            using (var context = new DbContextGeneric<T>())
            {
                return context.Set<T>().Find(id);

            }
        }


    }
}
