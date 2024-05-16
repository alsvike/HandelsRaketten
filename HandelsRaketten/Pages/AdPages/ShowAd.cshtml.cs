using HandelsRaketten.Areas.Identity.Data;
using HandelsRaketten.Data;
using HandelsRaketten.EFDBContext;
using HandelsRaketten.Models;
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HandelsHjornet.Pages.AdPages
{
    public class ShowAdModel : PageModel
    {
        IAdService _adService;
        ISellerService _sellerService;
        private readonly UserManager<User> _userManager;

        public string Category { get; set; }

        public Ad Ad { get; set; }
        public User CurrentUser { get; set; }

        public ShowAdModel(IAdService adService, ISellerService sellerService, UserManager<User> userManager)
        {
            _adService = adService;
            _sellerService = sellerService;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int adId)
        {

            Ad = await _adService.GetAdConversationAsync(adId);

            if (Ad == null)
            {
                return NotFound();
            }

            CurrentUser = await _userManager.GetUserAsync(User);

            return Page();
        }
    }
}
