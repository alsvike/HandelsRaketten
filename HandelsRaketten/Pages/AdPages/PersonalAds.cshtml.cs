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

        public async Task<IActionResult> OnGetAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);
            
            if(CurrentUser != null)
                PersonalAds = await _adService.GetAllByUserIdAsync(CurrentUser.Id);

            return Page();

        }
    }
}
