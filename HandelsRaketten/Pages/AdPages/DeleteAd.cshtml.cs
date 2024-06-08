
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HandelsHjornet.Pages.AdPages
{
    public class DeleteAdModel : PageModel
    {

        IAdService _adService;

        public Ad AdToBeDeleted { get; set; }
        public DeleteAdModel(IAdService adService)
        {
            _adService = adService;
        }

        // This method is the handler for HTTP GET requests to this Razor Page.
        public IActionResult OnGet(int adId, string category)
        {
            // Retrieve the advertisement object to be deleted from the database based on the provided adId.
            AdToBeDeleted = _adService.Get(adId);

            // Check if the advertisement object is null, indicating it was not found in the database.
            if (AdToBeDeleted == null)
            {
                // If the advertisement is not found, redirect to the Error page.
                return RedirectToPage("/Error");
            }

            // If the advertisement is found, return the Page.
            return Page();
        }

        // This method handles the HTTP POST request sent to this page, taking an adId as a parameter
        public async Task<IActionResult> OnPostAsync(int adId)
        {
            // Checks if the model state is not valid
            if (!ModelState.IsValid)
            {
                // If the model state is not valid, returns the current page
                return Page();
            }

            // Calls the DeleteAsync method of the _adService to delete the ad with the specified adId asynchronously
            await _adService.DeleteAsync(adId);

            // Redirects the user to the "ShowAllAds" page after successful deletion
            return RedirectToPage("PersonalAds");
        }

    }
}
