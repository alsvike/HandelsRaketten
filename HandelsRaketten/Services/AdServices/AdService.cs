using HandelsRaketten.Catalogs;
using HandelsRaketten.Data;
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using HandelsRaketten.Services.DbServices;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Reflection.Metadata.Ecma335;

namespace HandelsRaketten.Services.AdServices
{
    public class AdService : IAdService
    {
        private List<Ad> _objs;
        AdCatalog _adCatalog;
        PlantCatalog _plantCatalog;

        HandelsRakettenContext _context;
        IAdDbService _dbService;

        public AdService(AdCatalog adRepository, PlantCatalog plantRepository, HandelsRakettenContext context, IAdDbService dbService)
        {
            _dbService = dbService;
            _context = context;
            _adCatalog = adRepository;
            _plantCatalog = plantRepository;
            _objs = _dbService.GetObjectsAsync().Result.ToList();
        }

        public async Task<Ad> AddAsync(Ad obj, string category)
        {
            //if (obj != null)
            //{
            //    switch (category)
            //    {
            //        case "IndoorPlant":
            //            return await _indoorPlantCatalog.AddAsync((IndoorPlantAd)obj);
            //        case "OutdoorPlant":
            //            return await _outdoorPlantCatalog.AddAsync((OutdoorPlantAd)obj);
            //        default:
            //            return null;
            //    }
            //}
            return null;

        }

        public async Task<Ad> DeleteAsync(int adId, string category)
        {
            //switch (category)
            //{
            //    case "IndoorPlant":
            //        return await _indoorPlantCatalog.DeleteAsync((IndoorPlantAd)Get(adId, category));
            //    case "OutdoorPlant":
            //        return await _outdoorPlantCatalog.DeleteAsync((OutdoorPlantAd)Get(adId, category));
            //    default:
            //        return null;
            //}
            return null;

        }

        public Ad Get(int adId, string category)
        {
            //switch (category)
            //{
            //    case "IndoorPlant":
            //        return _indoorPlantCatalog.Get(adId);
            //    case "OutdoorPlant":
            //        return _outdoorPlantCatalog.Get(adId);
            //    default:
            //        return null;
            //}
            return null;
        }

        public async Task UpdateAsync(int adId, Ad obj, string category)
        {
            //if (obj != null)
            //{
            //    switch (category)
            //    {
            //        case "IndoorPlant":
            //            await _indoorPlantCatalog.EditAsync((IndoorPlantAd)obj, adId);
            //            break;
            //        case "OutdoorPlant":
            //            await _outdoorPlantCatalog.EditAsync((OutdoorPlantAd)obj, adId);
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }

        public async Task<List<Ad>> GetAllAsync(string category)
        {
            var ads = await _context.Set<Ad>()
                .Where(m => m.Category == category)
                .ToListAsync();

            return ads;
        }

        public async Task<List<Ad>> GetAllAdsAsync() => _objs = _dbService.GetObjectsAsync().Result.ToList();


        public async Task<Ad> GetAdConversationAsync(int adId, string category)
        {
            return await _dbService.GetAdConversationAsync(adId, category);
        }


        // sorting and filtering

        public IEnumerable<Ad> NameSearch(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return _objs;
            }
            var objsQuery = from obj in _objs
                            where obj.Title.ToLower().Contains(str.ToLower())
                            select obj;

            return objsQuery;
        }

        public IEnumerable<Ad> PriceFilter(int maxPrice, int minPrice = 0)
        {
            var objsQuery = from obj in _objs
                            where (obj.Price <= maxPrice && minPrice == 0) ||
                                  (obj.Price >= minPrice && maxPrice == 0) ||
                                  (obj.Price >= minPrice && obj.Price <= maxPrice)
                            select obj;
            return objsQuery;
        }
        public IEnumerable<Ad> SortByName()
        {
            // Sort Name Without Linq
            //_items.Sort(new NameComperator());
            //return _items;

            // Sort Name With Linq
            var namesQuery = from obj in _objs
                             orderby obj.Title
                             select obj;
            return namesQuery;
        }

        public IEnumerable<Ad> SortByNameDescending()
        {
            // Without linq
            //_items.Sort(new NameComperator());
            //return _items.Reverse<Item>();


            // With Linq
            var namesQuery = from obj in _objs
                             orderby obj.Title descending
                             select obj;
            return namesQuery;
        }

        public IEnumerable<Ad> SortByPrice()
        {
            // Without Linq
            //_items.Sort(new PriceComperator());
            //return _items;

            // With Linq
            var priceQuery = from obj in _objs
                             orderby obj.Price
                             select obj;
            return priceQuery;
        }

        public IEnumerable<Ad> SortByPriceDescending()
        {
            // Without Linq
            //_items.Sort(new PriceComperator());
            //return _items.Reverse<Item>();

            // With Linq
            var priceQuery = from obj in _objs
                             orderby obj.Price descending
                             select obj;
            return priceQuery;

        }
    }
}
