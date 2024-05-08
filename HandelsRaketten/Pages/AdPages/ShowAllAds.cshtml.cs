
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HandelsHjornet.Pages.TestPages
{
    public class ViewCategoriesTestPageModel : PageModel
    {
        IAdService _adService;
        [BindProperty]
        public string SelectedOption { get; set; }

        public List<Ad> _ads;
        public ViewCategoriesTestPageModel(IAdService adService)
        {
            _adService = adService;
        }

        public IActionResult OnGet()
        {
            _ads = _adService.GetAllAds();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ChangeCategory(SelectedOption);

            return Page();
        }

        private void ChangeCategory(string selectedOption)
        {
            switch (selectedOption)
            {
                case "All":
                    _ads = _adService.GetAll(selectedOption);
                    break;
                case "Plants":
                    _ads = _adService.GetAll(selectedOption);
                    break;
                case "IndoorPlant":
                    _ads = _adService.GetAll(selectedOption);
                    break;
                case "OutdoorPlant":
                    _ads = _adService.GetAll(selectedOption);
                    break;
                case "Tool":
                    _ads = _adService.GetAll(selectedOption);
                    break;
                case "GardeningTool":
                    _ads = _adService.GetAll(selectedOption);
                    break;
                case "PlantAccessories":
                    _ads = _adService.GetAll(selectedOption);
                    break;
                case "Soil":
                    _ads = _adService.GetAll(selectedOption);
                    break;
                case "Fertilizer":
                    _ads = _adService.GetAll(selectedOption);
                    break;
                default:
                    _ads = _adService.GetAllAds();
                    break;
            }
        }

    }
}
