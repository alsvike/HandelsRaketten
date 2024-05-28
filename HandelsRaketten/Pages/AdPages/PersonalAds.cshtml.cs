using HandelsRaketten.Areas.Identity.Data;
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HandelsRaketten.Pages.AdPages
{
    public class PersonalAdsModel : PageModel
    {

        UserManager<User> _userManager;

        IAdService _adService;

        public List<Ad>? PersonalAds { get; set; }
        public User? CurrentUser { get; set; }

        public PersonalAdsModel(UserManager<User> userManager, IAdService adService)
        {
            _userManager = userManager;
            _adService = adService;
        }

        // This method is executed when the corresponding Razor Page is requested using the HTTP GET method.
        public async Task<IActionResult> OnGetAsync()
        {
            // Retrieve the currently authenticated user asynchronously.
            CurrentUser = await _userManager.GetUserAsync(User);

            // If a user is currently authenticated, retrieve personal ads associated with the user's ID asynchronously.
            if (CurrentUser != null)
                PersonalAds = await _adService.GetAllByUserIdAsync(CurrentUser.Id);

            // Render the Razor Page associated with this method.
            return Page();
        }

    }
}
