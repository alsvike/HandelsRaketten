using HandelsRaketten.Areas.Identity.Data;
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

        private const string AdminRoleName = "Admin"; // Define the name of the admin role

        public bool IsAdmin { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //public void OnGet()
        //{
        //}

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            IsAdmin = await IsUserAdminAsync(user);
        }

        public async Task<bool> IsUserAdminAsync(User user)
        {
            if (user == null)
            {
                return false;
            }
            if (user.IsAdmin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
