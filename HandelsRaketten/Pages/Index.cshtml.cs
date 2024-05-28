using HandelsRaketten.Areas.Identity.Data;
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HandelsRaketten.Pages
{
    public class IndexModel : PageModel
    {
        public readonly SignInManager<User> _signInManager;
        public readonly UserManager<User> _userManager;
        private readonly ILogger<IndexModel> _logger;
        IAdService _adService;

        private const string AdminRoleName = "Admin"; // Define the name of the admin role

        public bool IsAdmin { get; private set; }
        public IEnumerable<Ad> Ads { get; set; }

        public IndexModel(ILogger<IndexModel> logger, SignInManager<User> signInManager, UserManager<User> userManager, IAdService adService)
        {
            _adService = adService;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // Define a public asynchronous method named "OnGetAsync"
        public async Task OnGetAsync()
        {
            // Await the asynchronous method call to retrieve all ads from the _adService
            Ads = await _adService.GetAllAdsAsync();

            // Await the asynchronous method call to retrieve the current user using the _userManager service
            var user = await _userManager.GetUserAsync(User);

            // Await the asynchronous method call to check if the current user is an admin
            IsAdmin = await IsUserAdminAsync(user);
        }

        // Define a method named IsUserAdminAsync that returns a Task<bool>. 
        // This method checks whether a given User object is an admin asynchronously.
        public async Task<bool> IsUserAdminAsync(User user)
        {
            // Check if the user object is null. If it is, return false.
            if (user == null)
            {
                return false;
            }
            // Check if the user's IsAdmin property is true. If it is, return true.
            if (user.IsAdmin)
            {
                return true;
            }
            // If the user is not null and is not an admin, return false.
            else
            {
                return false;
            }
        }

    }
}
