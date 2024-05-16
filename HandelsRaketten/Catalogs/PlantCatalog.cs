using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Data;
using Microsoft.EntityFrameworkCore;
using HandelsRaketten.Services.AdServices;

namespace HandelsRaketten.Catalogs
{
    public class PlantCatalog
    {

        //IAdService _adService;

        //public PlantCatalog(IAdService adService)
        //{
        //    _adService = adService;
        //}

        //public async Task<List<Ad>> GetAllAsync()
        //{

        //    var allPlantsAds = new List<Ad>();

        //    var indoorPlants = _context.IndoorPlant.ToList();
        //    if (indoorPlants != null)
        //        allPlantsAds.AddRange(indoorPlants);

        //    var outdoorPlants = _context.OutdoorPlant.ToList();
        //    if (outdoorPlants != null)
        //        allPlantsAds.AddRange(outdoorPlants);

        //    return allPlantsAds;
        //}
    }
}
