using HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HandelsHjornet.Pages.AdPages
{
    public class EditAdModel : PageModel
    {
        IAdService _adService;

        public string Category { get; set; }
        public int AdId { get; set; }


        [BindProperty] public IndoorPlant IndoorPlant { get; set; }
        [BindProperty] public OutdoorPlant OutdoorPlant { get; set; }
        [BindProperty] public Soil Soil { get; set; }
        [BindProperty] public Tool Tool { get; set; }
        [BindProperty] public GardeningTool GardeningTool { get; set; }
        [BindProperty] public Fertilizer Fertilizer { get; set; }

        public EditAdModel(IAdService adService)
        {
            _adService = adService;
        }

        public IActionResult OnGet(int adId, string category)
        {
            Category = category;
            AdId = adId;
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

        public IActionResult OnPost(int adId, string category)
        {
            switch (category)
            {
                case "IndoorPlant":
                    _adService.Edit(adId, IndoorPlant, category);
                    break;
                case "OutdoorPlant":
                    _adService.Edit(adId, OutdoorPlant, category);
                    break;
                case "Tool":
                    _adService.Edit(adId, Tool, category);
                    break;
                case "GardeningTool":
                    _adService.Edit(adId, GardeningTool, category);
                    break;
                case "Fertilizer":
                    _adService.Edit(adId, Fertilizer, category);
                    break;
                case "Soil":
                    _adService.Edit(adId, Soil, category);
                    break;
                default:
                    return RedirectToPage("/Error");
            }

            return RedirectToPage("ShowAllAds");
        }
    }
}
