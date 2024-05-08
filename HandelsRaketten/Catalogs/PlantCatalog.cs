using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using HandelsRaketten.Models.AdModels;

namespace HandelsRaketten.Catalogs
{
    public class PlantCatalog
    {
        public GenericCatalog<IndoorPlant> IndoorPlantCatalog { get; set; }
        public GenericCatalog<OutdoorPlant> OutdoorPlantCatalog { get; set; }

        public PlantCatalog(GenericCatalog<IndoorPlant> indoorPlantCatalog, GenericCatalog<OutdoorPlant> outdoorPlantCatalog)
        {
            IndoorPlantCatalog = indoorPlantCatalog;
            OutdoorPlantCatalog = outdoorPlantCatalog;
        }

        public List<Ad> GetAll()
        {

            var allPlantsAds = new List<Ad>();

            var indoorAds = IndoorPlantCatalog.GetAll();
            if (indoorAds.Any())
            {
                allPlantsAds.AddRange(indoorAds);
            }

            var outdoorAds = OutdoorPlantCatalog.GetAll();
            if (outdoorAds.Any())
            {
                allPlantsAds.AddRange(outdoorAds);
            }

            return allPlantsAds;
        }
    }
}
