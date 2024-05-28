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

        // Define a public method called OnGet which takes two parameters:
        // 1. An integer representing an ad ID.
        // 2. A string representing the subcategory of the ad.
        public IActionResult OnGet(int adId, string subCategory)
        {
            // Assign the value of subCategory to the SubCategory property of the current class.
            SubCategory = subCategory;

            // Start a switch statement based on the value of subCategory.
            switch (subCategory)
            {
                // If subCategory is "IndoorPlant", execute the following block.
                case "IndoorPlant":
                    // Retrieve the ad with the specified adId from the _adService and cast it to an IndoorPlantAd.
                    IndoorPlantAd = (IndoorPlantAd)_adService.Get(adId);
                    // Exit the switch statement.
                    break;

                // If subCategory is "OutdoorPlant", execute the following block.
                case "OutdoorPlant":
                    // Retrieve the ad with the specified adId from the _adService and cast it to an OutdoorPlantAd.
                    OutdoorPlantAd = (OutdoorPlantAd)_adService.Get(adId);
                    // Exit the switch statement.
                    break;
            }

            // Return the Page associated with this handler method.
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int adId, string subCategory) // Declaring a method to handle POST requests with parameters adId and subCategory
        {

            if (!ModelState.IsValid) // Checking if the model state is not valid
            {
                return Page(); // Returning the current page if model state is not valid
            }

            switch (subCategory) // Switching based on the value of subCategory parameter
            {
                case "IndoorPlant": // If subCategory is "IndoorPlant"
                    await _adService.UpdateAsync(adId, IndoorPlantAd); // Calling UpdateAsync method of _adService with adId and IndoorPlantAd
                    break; // Exiting the switch statement
                case "OutdoorPlant": // If subCategory is "OutdoorPlant"
                    await _adService.UpdateAsync(adId, OutdoorPlantAd); // Calling UpdateAsync method of _adService with adId and OutdoorPlantAd
                    break; // Exiting the switch statement
            }

            return RedirectToPage("ShowAllAds"); // Returning a redirect to the "ShowAllAds" page
        }
    }
}
