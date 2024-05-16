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

        public string Category { get; set; }
        public int AdId { get; set; }


        [BindProperty] public Ad Ad { get; set; }

        public EditAdModel(IAdService adService)
        {
            _adService = adService;
        }

        public IActionResult OnGet(int adId, string category)
        {
            Category = category;
            AdId = adId;


            return Page();
        }

        public IActionResult OnPost(int adId, string category)
        {


            return RedirectToPage("ShowAllAds");
        }
    }
}
