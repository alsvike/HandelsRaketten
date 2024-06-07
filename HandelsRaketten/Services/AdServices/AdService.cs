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

        // Define a public asynchronous method named AddAsync which takes an object of type Ad as a parameter.
        public async Task AddAsync(Ad obj)
        {
            // Check if the input object is not null.
            if (obj != null)
            {
                // Add the object to a collection named _objs.
                _objs.Add(obj);

                // Call an asynchronous method named AddObjectAsync from an object named _dbService and pass the object as a parameter.
                await _dbService.AddObjectAsync(obj);
            }

        }

        public async Task<Ad> DeleteAsync(int adId)
        {
            // Find the ad with the specified ID in the collection.
            var ad = _objs.FirstOrDefault(a => a.Id == adId);

            // If the ad is found.
            if (ad != null)
            {
                // Remove the ad from the collection.
                _objs.Remove(ad);

                // Asynchronously delete the ad from the database.
                await _dbService.DeleteObjectAsync(ad);

                // Return the deleted ad.
                return ad;
            }

            // Return null if the ad is not found.
            return null;
        }

        public Ad Get(int adId)
        {
            // Using LINQ, find and return the first Ad object in the collection '_objs' 
            // where the Id property matches the provided 'adId'.
            return _objs.FirstOrDefault(a => a.Id == adId);
        }

        // --- START OF UPDATE METHOD --- //

        // Define a method named UpdateAsync that returns a Task. 
        // It takes two parameters: an integer adId and an object of type Ad.
        public async Task UpdateAsync(int adId, Ad obj)
        {
            // Search for an ad object in the _objs collection whose Id matches the given adId.
            var ad = _objs.FirstOrDefault(a => a.Id == adId);

            // Check if an ad object was found.
            if (ad != null)
            {
                // Call a method to update common fields between the existing ad object and the new object.
                UpdateCommonFields(ad, obj);

                // Call a method to update specific fields of the existing ad object based on the new object.
                UpdateSpecificFields(ad, obj);

                // Asynchronously update the ad object in the database using a database service.
                await _dbService.UpdateObjectAsync(ad);
            }

        }


        // Update fields common to all ads
        // This method updates common fields of an ad object with values from another ad object.
        private void UpdateCommonFields(Ad ad, Ad obj)
        {
            // Assigns the title of the source ad object to the title of the destination ad object.
            ad.Title = obj.Title;

            // Assigns the price of the source ad object to the price of the destination ad object.
            ad.Price = obj.Price;

            // Assigns the description of the source ad object to the description of the destination ad object.
            ad.Description = obj.Description;

            // Assigns the image of the source ad object to the image of the destination ad object.
            ad.AdImage = obj.AdImage;
        }


        // Update Fields specific to each category of ad
        // This method updates specific fields of an advertisement based on its type.
        private void UpdateSpecificFields(Ad ad, Ad obj)
        {
            // Switching based on the type of ad passed in.
            switch (ad)
            {
                // If the ad is an OutdoorPlantAd and the obj is also an OutdoorPlantAd,
                // update the fields using the method UpdateOutdoorPlantAdFields.
                case OutdoorPlantAd outdoorPlantAd when obj is OutdoorPlantAd newOutdoorPlantAd:
                    UpdateOutdoorPlantAdFields(outdoorPlantAd, newOutdoorPlantAd);
                    break;
                // If the ad is an IndoorPlantAd and the obj is also an IndoorPlantAd,
                // update the fields using the method UpdateIndoorPlantAdFields.
                case IndoorPlantAd indoorPlantAd when obj is IndoorPlantAd newIndoorPlantAd:
                    UpdateIndoorPlantAdFields(indoorPlantAd, newIndoorPlantAd);
                    break;
            }
        }


        // Category Outdoorplant
        // Method for updating fields of an outdoor plant advertisement
        private void UpdateOutdoorPlantAdFields(OutdoorPlantAd ad, OutdoorPlantAd obj)
        {
            // Update the species field of the advertisement with the value from the object
            ad.Species = obj.Species;

            // Update the recommended soil field of the advertisement with the value from the object
            ad.RecommendedSoil = obj.RecommendedSoil;

            // Update the flowering season field of the advertisement with the value from the object
            ad.FloweringSeason = obj.FloweringSeason;

            // Update the size field of the advertisement with the value from the object
            ad.Size = obj.Size;
        }

        // Category IndoorPlant
        // This method updates the fields of an IndoorPlantAd object with the values from another IndoorPlantAd object.

        private void UpdateIndoorPlantAdFields(IndoorPlantAd ad, IndoorPlantAd obj)
        {
            // Assigns the PotSize property of the destination IndoorPlantAd object with the value of the PotSize property from the source IndoorPlantAd object.
            ad.PotSize = obj.PotSize;

            // Assigns the Size property of the destination IndoorPlantAd object with the value of the Size property from the source IndoorPlantAd object.
            ad.Size = obj.Size;

            // Assigns the WateringNeeds property of the destination IndoorPlantAd object with the value of the WateringNeeds property from the source IndoorPlantAd object.
            ad.WateringNeeds = obj.WateringNeeds;

            // Assigns the SunlightNeeds property of the destination IndoorPlantAd object with the value of the SunlightNeeds property from the source IndoorPlantAd object.
            ad.SunlightNeeds = obj.SunlightNeeds;

            // Assigns the Species property of the destination IndoorPlantAd object with the value of the Species property from the source IndoorPlantAd object.
            ad.Species = obj.Species;

            // Assigns the RecommendedSoil property of the destination IndoorPlantAd object with the value of the RecommendedSoil property from the source IndoorPlantAd object.
            ad.RecommendedSoil = obj.RecommendedSoil;

            // Assigns the RecommendedTemperature property of the destination IndoorPlantAd object with the value of the RecommendedTemperature property from the source IndoorPlantAd object.
            ad.RecommendedTemperature = obj.RecommendedTemperature;
        }


        // --- END OF UPDATE METHOD --- //

        // Retrieves advertisements by category asynchronously.
        public async Task<List<Ad>> GetAllByCategoryAsync(string category)
        {
            return await _dbService.GetAllByCategoryAsync(category);
        }

        // Retrieves advertisements by user ID asynchronously.
        public async Task<List<Ad>> GetAllByUserIdAsync(string userId)
        {
            return await _dbService.GetAllByUserIdAsync(userId);
        }

        // Retrieves advertisements by subcategory asynchronously.
        public async Task<List<Ad>> GetAllBySubcategoryAsync(string discriminator)
        {
            return await _dbService.GetAllBySubcategoryAsync(discriminator);
        }

        // Retrieves all advertisements asynchronously.
        public async Task<List<Ad>> GetAllAdsAsync() => _objs = _dbService.GetObjectsAsync().Result.ToList();

        // Retrieves a conversation related to a specific advertisement asynchronously.
        public async Task<Ad> GetAdConversationAsync(int adId)
        {
            return await _dbService.GetAdConversationAsync(adId);
        }

        public async Task DeleteMessageAsync(int msgId)
        {
            if(msgId > 0 )
            await _dbService.DeleteMessageAsync(msgId);
        }

        // sorting and filtering
        // Searches for advertisements by name.
        public IEnumerable<Ad> NameSearch(string str)
        {
            // If the search string is null or empty, return all advertisements.
            if (string.IsNullOrEmpty(str))
            {
                return _objs;
            }
            // Otherwise, filter advertisements by title containing the search string.
            var objsQuery = from obj in _objs
                            where obj.Title.ToLower().Contains(str.ToLower())
                            select obj;

            return objsQuery;
        }

        // Filters advertisements by price range.
        public IEnumerable<Ad> PriceFilter(int maxPrice, int minPrice = 0)
        {
            // Define a query to filter advertisements by price range.
            var objsQuery = from obj in _objs
                            where (obj.Price <= maxPrice && minPrice == 0) ||  // If only max price is specified
                                  (obj.Price >= minPrice && maxPrice == 0) ||  // If only min price is specified
                                  (obj.Price >= minPrice && obj.Price <= maxPrice)  // If both min and max prices are specified
                            select obj;
            return objsQuery;
        }

        // Sorts advertisements by name in ascending order.
        public IEnumerable<Ad> SortByName()
        {
            var namesQuery = from obj in _objs
                             orderby obj.Title
                             select obj;
            return namesQuery;
        }

        // Sorts advertisements by name in descending order.
        public IEnumerable<Ad> SortByNameDescending()
        {
            var namesQuery = from obj in _objs
                             orderby obj.Title descending
                             select obj;
            return namesQuery;
        }

        // Sorts advertisements by price in ascending order.
        public IEnumerable<Ad> SortByPrice()
        {
            var priceQuery = from obj in _objs
                             orderby obj.Price
                             select obj;
            return priceQuery;
        }

        // Sorts advertisements by price in descending order.
        public IEnumerable<Ad> SortByPriceDescending()
        {
            var priceQuery = from obj in _objs
                             orderby obj.Price descending
                             select obj;
            return priceQuery;
        }

    }
}
