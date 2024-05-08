using HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HandelsHjornet.Pages.AdPages
{
    public class ShowAdModel : PageModel
    {
        IAdService _adService;

        public string Category { get; set; }

        public IndoorPlant IndoorPlant { get; set; }
        public OutdoorPlant OutdoorPlant { get; set; }
        public Soil Soil { get; set; }
        public Tool Tool { get; set; }
        public GardeningTool GardeningTool { get; set; }
        public Fertilizer Fertilizer { get; set; }

        public ShowAdModel(IAdService adService)
        {
            _adService = adService;
        }

        public IActionResult OnGet(int adId, string category)
        {

            Category = category;
            switch (category)
            {
                case "IndoorPlant":
                    IndoorPlant = (IndoorPlant)_adService.Get(adId, category);
                    break;
                case "OutdoorPlant":
                    OutdoorPlant = (OutdoorPlant)_adService.Get(adId, category);
                    break;
                case "Tool":
                    Tool = (Tool)_adService.Get(adId, category);
                    break;
                case "GardeningTool":
                    GardeningTool = (GardeningTool)_adService.Get(adId, category);
                    break;
                case "Fertilizer":
                    Fertilizer = (Fertilizer)_adService.Get(adId, category);
                    break;
                case "Soil":
                    Soil = (Soil)_adService.Get(adId, category);
                    break;
                default:
                    return RedirectToPage("/Error");
            }

            return Page();
        }
    }
}
