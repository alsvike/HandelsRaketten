using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HandelsHjornet.Pages.AdPages
{
    public class EditAdModel : PageModel
    {
        IAdService _adService;

        [BindProperty]public IndoorPlantAd IndoorPlantAd { get; set; }
        [BindProperty]public OutdoorPlantAd OutdoorPlantAd { get; set; }

        public string SubCategory { get; set; }
        public EditAdModel(IAdService adService)
        {
            _adService = adService;
        }

        public IActionResult OnGet(int adId, string subCategory)
        {
            SubCategory = subCategory;
            switch (subCategory)
            {
                case "IndoorPlant":
                    IndoorPlantAd = (IndoorPlantAd)_adService.Get(adId);
                    break;
                case "OutdoorPlant":
                    OutdoorPlantAd = (OutdoorPlantAd)_adService.Get(adId);
                    break;
            }
            return Page();
        }

        public IActionResult OnPost(int adId, string subCategory)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            switch (subCategory)
            {
                case "IndoorPlant":
                    _adService.UpdateAsync(adId, IndoorPlantAd);
                    break;
                case "OutdoorPlant":
                    _adService.UpdateAsync(adId, OutdoorPlantAd);
                    break;
            }


            return RedirectToPage("ShowAllAds");
        }
    }
}
