using HandelsRaketten.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HandelsRaketten.Pages
{
    public class IndexModel : PageModel
    {
        public readonly SignInManager<User> _signInManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, SignInManager<User> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        public void OnGet()
        {

        }
    }
}
