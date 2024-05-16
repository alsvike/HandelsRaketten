
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

        [BindProperty]
        public string? SearchString { get; set; }

        [BindProperty]
        public int MinPrice { get; set; }

        [BindProperty]
        public int MaxPrice { get; set; }

        public List<Ad> _ads;


        bool _isNameSorted;
        bool _isPriceSorted;
        public ViewCategoriesTestPageModel(IAdService adService)
        {
            _adService = adService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            _ads = await _adService.GetAllAdsAsync();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ChangeCategoryAsync(SelectedOption);

            return Page();
        }

        private async Task ChangeCategoryAsync(string selectedOption)
        {
            switch (selectedOption)
            {
                case "All":
                    _ads = await _adService.GetAllAsync(selectedOption);
                    break;
                case "Plants":
                    _ads = await _adService.GetAllAsync(selectedOption);
                    break;
                case "IndoorPlant":
                    _ads = await _adService.GetAllAsync(selectedOption);
                    break;
                case "OutdoorPlant":
                    _ads = await _adService.GetAllAsync(selectedOption);
                    break;
                case "Tool":
                    _ads = await _adService.GetAllAsync(selectedOption);
                    break;
                case "GardeningTool":
                    _ads = await _adService.GetAllAsync(selectedOption);
                    break;
                case "PlantAccessories":
                    _ads = await _adService.GetAllAsync(selectedOption);
                    break;
                case "Soil":
                    _ads = await _adService.GetAllAsync(selectedOption);
                    break;
                case "Fertilizer":
                    _ads = await _adService.GetAllAsync(selectedOption);
                    break;
                default:
                    _ads = await _adService.GetAllAdsAsync();
                    break;
            }
        }

        public IActionResult OnGetSortName()
        {
            if (TempData.ContainsKey("IsNameSorted"))
            {
                _isNameSorted = (bool)TempData["IsNameSorted"];
            }

            if (_isNameSorted)
            {
                _ads = _adService.SortByNameDescending().ToList();
                _isNameSorted = false;
            }
            else
            {
                _ads = _adService.SortByName().ToList();
                _isNameSorted = true;
            }

            TempData["IsNameSorted"] = _isNameSorted;

            return Page();
        }


        public IActionResult OnGetSortPrice()
        {
            if (TempData.ContainsKey("IsPriceSorted"))
            {
                _isPriceSorted = (bool)TempData["IsPriceSorted"];
            }

            if (_isPriceSorted)
            {
                _ads = _adService.SortByPriceDescending().ToList();
                _isPriceSorted = false;
            }
            else
            {
                _ads = _adService.SortByPrice().ToList();
                _isPriceSorted = true;
            }

            TempData["IsPriceSorted"] = _isPriceSorted;

            return Page();
        }


        public IActionResult OnPostNameSearch()
        {
            _ads = _adService.NameSearch(SearchString).ToList();
            return Page();
        }

        public IActionResult OnPostPriceFilter()
        {
            _ads = _adService.PriceFilter(MaxPrice, MinPrice).ToList();
            return Page();
        }

    }
}
