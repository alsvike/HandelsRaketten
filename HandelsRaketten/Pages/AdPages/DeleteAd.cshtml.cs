
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

        public IActionResult OnGet(int adId, string category)
        {

            AdToBeDeleted = _adService.Get(adId);

            if(AdToBeDeleted == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public IActionResult OnPost(int adId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _adService.DeleteAsync(adId);

            return RedirectToPage("ShowAllAds");
        }
    }
}
