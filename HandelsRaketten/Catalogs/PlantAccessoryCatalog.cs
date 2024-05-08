using HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories;
using HandelsRaketten.Models.AdModels;

namespace HandelsRaketten.Catalogs
{
    public class PlantAccessoryCatalog
    {
        public GenericCatalog<Tool> ToolCatalog { get; set; }
        public GenericCatalog<GardeningTool> GardeningToolCatalog { get; set; }
        public GenericCatalog<Soil> SoilCatalog { get; set; }
        public GenericCatalog<Fertilizer> FertilizerCatalog { get; set; }

        public PlantAccessoryCatalog(GenericCatalog<Tool> toolRepository, GenericCatalog<GardeningTool> gardeningToolRepository, GenericCatalog<Soil> soilRepository, GenericCatalog<Fertilizer> fertilizerRepository)
        {
            ToolCatalog = toolRepository;
            GardeningToolCatalog = gardeningToolRepository;
            SoilCatalog = soilRepository;
            FertilizerCatalog = fertilizerRepository;
        }

        public List<Ad> GetAll()
        {

            var allPlantAccessoryAds = new List<Ad>();

            // Fetch advertisements for tools
            var toolAds = ToolCatalog.GetAll();
            if (toolAds.Any())
            {
                allPlantAccessoryAds.AddRange(toolAds);
            }

            // Fetch advertisements for fertilizers
            var fertilizerAds = FertilizerCatalog.GetAll();
            if (fertilizerAds.Any())
            {
                allPlantAccessoryAds.AddRange(fertilizerAds);
            }

            // Fetch advertisements for gardening tools
            var gardeningToolAds = GardeningToolCatalog.GetAll();
            if (gardeningToolAds.Any())
            {
                allPlantAccessoryAds.AddRange(gardeningToolAds);
            }

            // Fetch advertisements for soil
            var soilAds = SoilCatalog.GetAll();
            if (soilAds.Any())
            {
                allPlantAccessoryAds.AddRange(soilAds);
            }

            return allPlantAccessoryAds;
        }
    }
}
