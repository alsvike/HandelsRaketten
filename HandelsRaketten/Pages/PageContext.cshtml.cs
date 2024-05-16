using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HandelsRaketten.Pages
{
    public class PageContextModel : PageModel
    {
        IAdService _adService;
        [BindProperty]
        public string SelectedOption { get; set; }

        [BindProperty]
        public string? SearchString { get; set; }

        [BindProperty]
        public int MinPrice { get; set; }

        [BindProperty]
        public int MaxPrice { get; set; }

        public List<Ad> _ads;
        public PageContextModel(IAdService adService)
        {
            _adService = adService;
        }

        public async Task<IActionResult> OnGetAsync(string category)
        {
            switch (category)
            {
                case "All":
                    _ads = await _adService.GetAllAsync(category);
                    break;
                case "Plants":
                    _ads = await _adService.GetAllAsync(category);
                    break;
                case "IndoorPlant":
                    _ads = await _adService.GetAllAsync(category);
                    break;
                case "OutdoorPlant":
                    _ads = await _adService.GetAllAsync(category);
                    break;
                default:
                    _ads = await _adService.GetAllAdsAsync();
                    break;
            }
            return Page();
        }
    }
}