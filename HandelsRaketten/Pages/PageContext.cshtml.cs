using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HandelsRaketten.Pages
{
    public class PageContextModel : PageModel
    {
        IAdService _adService;

        public List<Ad> _ads;
        public PageContextModel(IAdService adService)
        {
            _adService = adService;
        }

        public IActionResult OnGet(string category)
        {
            switch (category)
            {
                case "All":
                    _ads = _adService.GetAll(category);
                    break;
                case "Plants":
                    _ads = _adService.GetAll(category);
                    break;
                case "IndoorPlant":
                    _ads = _adService.GetAll(category);
                    break;
                case "OutdoorPlant":
                    _ads = _adService.GetAll(category);
                    break;
                case "Tool":
                    _ads = _adService.GetAll(category);
                    break;
                case "GardeningTool":
                    _ads = _adService.GetAll(category);
                    break;
                case "PlantAccessories":
                    _ads = _adService.GetAll(category);
                    break;
                case "Soil":
                    _ads = _adService.GetAll(category);
                    break;
                case "Fertilizer":
                    _ads = _adService.GetAll(category);
                    break;
                default:
                    _ads = _adService.GetAllAds();
                    break;
            }

            return Page();
        }
    }
}
