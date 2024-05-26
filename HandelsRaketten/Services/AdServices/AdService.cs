using HandelsRaketten.Catalogs;
using HandelsRaketten.Data;
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using HandelsRaketten.Services.DbServices;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Remoting;

namespace HandelsRaketten.Services.AdServices
{
    public class AdService : IAdService
    {
        private List<Ad> _objs;

        IAdDbService _dbService;

        public AdService(IAdDbService dbService)
        {
            _dbService = dbService;

            _objs = _dbService.GetObjectsAsync().Result.ToList();
        }

        public async Task AddAsync(Ad obj)
        {
            if(obj != null)
            {
                _objs.Add(obj);
                await _dbService.AddObjectAsync(obj);
            }

        }

        // Ad A Message to the Ad
        public async Task AddMessageAsync(int adId, Message message)
        {
            //if(message != null)
            //{
            //    // get the Ad where the message should be added
            //    var ad = Get(adId);

            //    if(ad.Messages == null)
            //    {
            //        ad.Messages = new List<Message>();
            //    }
            //    // ad the message directly to the ads messages, so that it can be displayed without a restart
            //    ad.Messages.Add(message);


            //    // save the message in the database
            //    await _dbService.AddMessage(message);
            //}
        }


        public async Task<Ad> DeleteAsync(int adId)
        {
            var ad = _objs.FirstOrDefault(a => a.Id == adId);

            if(ad != null)
            {
                _objs.Remove(ad);
                await _dbService.DeleteObjectAsync(ad);
                return ad;
            }
            return null;

        }

        public Ad Get(int adId)
        {

            return _objs.FirstOrDefault(a => a.Id == adId);
        }

        // --- START OF UPDATE METHOD --- //

        public async Task UpdateAsync(int adId, Ad obj)
        {
            var ad = _objs.FirstOrDefault(a => a.Id == adId);

            if (ad != null)
            {
                UpdateCommonFields(ad, obj);
                UpdateSpecificFields(ad, obj);
                await _dbService.UpdateObjectAsync(ad);
            }

        }

        // Update fields common to all ads
        private void UpdateCommonFields(Ad ad, Ad obj)
        {
            ad.Title = obj.Title;
            ad.Price = obj.Price;
            ad.Description = obj.Description;
            ad.AdImage = obj.AdImage;
        }

        // Update Fields specific to each category of ad
        private void UpdateSpecificFields(Ad ad, Ad obj)
        {
            switch (ad)
            {
                case OutdoorPlantAd outdoorPlantAd when obj is OutdoorPlantAd newOutdoorPlantAd:
                    UpdateOutdoorPlantAdFields(outdoorPlantAd, newOutdoorPlantAd);
                    break;
                case IndoorPlantAd indoorPlantAd when obj is IndoorPlantAd newIndoorPlantAd:
                    UpdateIndoorPlantAdFields(indoorPlantAd, newIndoorPlantAd);
                    break;
            }
        }

        // Category Outdoorplant
        private void UpdateOutdoorPlantAdFields(OutdoorPlantAd ad, OutdoorPlantAd obj)
        {
            ad.Species = obj.Species;
            ad.RecommendedSoil = obj.RecommendedSoil;
            ad.FloweringSeason = obj.FloweringSeason;
            ad.Size = obj.Size;
        }

        // Category IndoorPlant
        private void UpdateIndoorPlantAdFields(IndoorPlantAd ad, IndoorPlantAd obj)
        {
            ad.PotSize = obj.PotSize;
            ad.Size = obj.Size;
            ad.WateringNeeds = obj.WateringNeeds;
            ad.SunlightNeeds = obj.SunlightNeeds;
            ad.Species = obj.Species;
            ad.RecommendedSoil = obj.RecommendedSoil;
            ad.RecommendedTemperature = obj.RecommendedTemperature;
        }

        // --- END OF UPDATE METHOD --- //

        public async Task<List<Ad>> GetAllByCategoryAsync(string category)
        {
            return await _dbService.GetAllByCategoryAsync(category);
        }

        public async Task<List<Ad>> GetAllBySubcategoryAsync(string discriminator)
        {
            return await _dbService.GetAllBySubcategoryAsync(discriminator);
        }

        public async Task<List<Ad>> GetAllAdsAsync() => _objs = _dbService.GetObjectsAsync().Result.ToList();


        public async Task<Ad> GetAdConversationAsync(int adId)
        {
            return await _dbService.GetAdConversationAsync(adId);
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

            var namesQuery = from obj in _objs
                             orderby obj.Title
                             select obj;
            return namesQuery;
        }

        public IEnumerable<Ad> SortByNameDescending()
        {

            var namesQuery = from obj in _objs
                             orderby obj.Title descending
                             select obj;
            return namesQuery;
        }

        public IEnumerable<Ad> SortByPrice()
        {
            var priceQuery = from obj in _objs
                             orderby obj.Price
                             select obj;
            return priceQuery;
        }

        public IEnumerable<Ad> SortByPriceDescending()
        {

            var priceQuery = from obj in _objs
                             orderby obj.Price descending
                             select obj;
            return priceQuery;

        }
    }
}
