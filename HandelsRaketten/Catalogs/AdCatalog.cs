using HandelsRaketten.Models.AdModels;

namespace HandelsRaketten.Catalogs
{
    public class AdCatalog
    {
        PlantCatalog Plants;
        PlantAccessoryCatalog PlantAccessories;

        public AdCatalog(PlantCatalog plants, PlantAccessoryCatalog plantAccessories)
        {
            Plants = plants;
            PlantAccessories = plantAccessories;
        }

        public List<Ad> GetAll()
        {
            var allAds = new List<Ad>();

            var allPlants = Plants.GetAll();
            if (allPlants.Any())
                allAds.AddRange(allPlants);

            var allPlantAccessories = PlantAccessories.GetAll();
            if (allPlantAccessories.Any())
                allAds.AddRange(allPlantAccessories);

            return allAds;
        }


    }
}
