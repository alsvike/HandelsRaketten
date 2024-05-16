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


        [BindProperty] public IndoorPlantAd IndoorPlant { get; set; }
        [BindProperty] public OutdoorPlantAd OutdoorPlant { get; set; }

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
                    IndoorPlant = (IndoorPlantAd)_adService.Get(adId, category);
                    break;
                case "OutdoorPlant":
                    OutdoorPlant = (OutdoorPlantAd)_adService.Get(adId, category);
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
                    _adService.UpdateAsync(adId, IndoorPlant, category);
                    break;
                case "OutdoorPlant":
                    _adService.UpdateAsync(adId, OutdoorPlant, category);
                    break;
                default:
                    return RedirectToPage("/Error");
            }

            return RedirectToPage("ShowAllAds");
        }
    }
}
