using HandelsRaketten.Catalogs;
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using NuGet.Protocol.Core.Types;

namespace HandelsRaketten.Services.AdServices
{
    public class AdService : IAdService
    {
        private List<Ad> _objs;
        AdCatalog _adCatalog;
        PlantCatalog _plantCatalog;
        PlantAccessoryCatalog _plantAccessoryCatalog;

        GenericCatalog<IndoorPlant> _indoorPlantCatalog;
        GenericCatalog<OutdoorPlant> _outdoorPlantCatalog;
        GenericCatalog<Soil> _soilCatalog;
        GenericCatalog<Fertilizer> _fertilizerCatalog;
        GenericCatalog<Tool> _toolCatalog;
        GenericCatalog<GardeningTool> _gardeningToolCatalog;



        public AdService(AdCatalog adRepository, PlantCatalog plantRepository, PlantAccessoryCatalog plantAccessoryRepository, GenericCatalog<IndoorPlant> indoorPlantCatalog, GenericCatalog<OutdoorPlant> outdoorPlantCatalog, GenericCatalog<Soil> soilCatalog, GenericCatalog<Fertilizer> fertilizerCatalog, GenericCatalog<Tool> toolCatalog, GenericCatalog<GardeningTool> gardeningToolCatalog)
        {
            _adCatalog = adRepository;
            _plantCatalog = plantRepository;
            _plantAccessoryCatalog = plantAccessoryRepository;
            _indoorPlantCatalog = indoorPlantCatalog;
            _outdoorPlantCatalog = outdoorPlantCatalog;
            _soilCatalog = soilCatalog;
            _fertilizerCatalog = fertilizerCatalog;
            _toolCatalog = toolCatalog;
            _gardeningToolCatalog = gardeningToolCatalog;
            _objs = _adCatalog.GetAll();
        }

        public Ad Add(Ad obj, string category)
        {
            if (obj != null)
            {
                switch (category)
                {
                    case "IndoorPlant":
                        return _indoorPlantCatalog.Add((IndoorPlant)obj);
                    case "OutdoorPlant":
                        return _outdoorPlantCatalog.Add((OutdoorPlant)obj);
                    case "Tool":
                        return _toolCatalog.Add((Tool)obj);
                    case "GardeningTool":
                        return _gardeningToolCatalog.Add((GardeningTool)obj);
                    case "Fertilizer":
                        return _fertilizerCatalog.Add((Fertilizer)obj);
                    case "Soil":
                        return _soilCatalog.Add((Soil)obj);
                    default:
                        return null;
                }
            }
            return null;

        }

        public Ad Delete(int adId, string category)
        {
            switch (category)
            {
                case "IndoorPlant":
                    return _indoorPlantCatalog.Delete((IndoorPlant)Get(adId, category));
                case "OutdoorPlant":
                    return _outdoorPlantCatalog.Delete((OutdoorPlant)Get(adId, category));
                case "Tool":
                    return _toolCatalog.Delete((Tool)Get(adId, category));
                case "GardeningTool":
                    return _gardeningToolCatalog.Delete((GardeningTool)Get(adId, category));
                case "Fertilizer":
                    return _fertilizerCatalog.Delete((Fertilizer)Get(adId, category));
                case "Soil":
                    return _soilCatalog.Delete((Soil)Get(adId, category));
                default:
                    return null;
            }

        }

        public Ad Get(int adId, string category)
        {
            switch (category)
            {
                case "IndoorPlant":
                    return _indoorPlantCatalog.Get(adId);
                case "OutdoorPlant":
                    return _outdoorPlantCatalog.Get(adId);
                case "Tool":
                    return _toolCatalog.Get(adId);
                case "GardeningTool":
                    return _gardeningToolCatalog.Get(adId);
                case "Fertilizer":
                    return _fertilizerCatalog.Get(adId);
                case "Soil":
                    return _soilCatalog.Get(adId);
                default:
                    return null;
            }
        }

        public void Edit(int adId, Ad obj, string category)
        {
            if (obj != null)
            {
                switch (category)
                {
                    case "IndoorPlant":
                        _indoorPlantCatalog.Edit((IndoorPlant)obj, adId);
                        break;
                    case "OutdoorPlant":
                        _outdoorPlantCatalog.Edit((OutdoorPlant)obj, adId);
                        break;
                    case "Tool":
                        _toolCatalog.Edit((Tool)obj, adId);
                        break;
                    case "GardeningTool":
                        _gardeningToolCatalog.Edit((GardeningTool)obj, adId);
                        break;
                    case "Fertilizer":
                        _fertilizerCatalog.Edit((Fertilizer)obj, adId);
                        break;
                    case "Soil":
                        _soilCatalog.Edit((Soil)obj, adId);
                        break;
                    default:
                        break;
                }
            }
        }

        public List<Ad> GetAll(string category)
        {
            switch (category)
            {
                case "All":
                    return _adCatalog.GetAll();
                case "Plants":
                    return _plantCatalog.GetAll();
                case "PlantAccessories":
                    return _plantAccessoryCatalog.GetAll();
                case "IndoorPlant":
                    return _indoorPlantCatalog.GetAll();
                case "OutdoorPlant":
                    return _outdoorPlantCatalog.GetAll();
                case "Tool":
                    return _toolCatalog.GetAll();
                case "GardeningTool":
                    return _gardeningToolCatalog.GetAll();
                case "Fertilizer":
                    return _fertilizerCatalog.GetAll();
                case "Soil":
                    return _soilCatalog.GetAll();
                default:
                    return _adCatalog.GetAll();
            }
        }

        public List<Ad> GetAllAds() => _objs = _adCatalog.GetAll();


        // sorting and filtering

        public IEnumerable<Ad> NameSearch(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return _objs;
            }
            var objsQuery = from obj in _objs
                            where obj.Name.ToLower().Contains(str.ToLower())
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
                             orderby obj.Name
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
                             orderby obj.Name descending
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
