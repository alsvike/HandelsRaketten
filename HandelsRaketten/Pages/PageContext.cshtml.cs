using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace HandelsRaketten.Pages
{
    public class PageContextModel : PageModel
    {
        private readonly IAdService _adService;
        [BindProperty(SupportsGet = true)]
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
            // Set the selected option to the provided category.
            SelectedOption = category;

            // Check if the category is not null or empty.
            if (!string.IsNullOrEmpty(category))
            {
                // If category is provided, determine action based on its value using a switch statement.
                switch (category)
                {
                    case "Plants":
                        // Retrieve all ads for the "Plants" category.
                        _ads = await _adService.GetAllByCategoryAsync(category);
                        break;
                    case "IndoorPlant":
                        // Retrieve all ads for the "IndoorPlant" subcategory.
                        _ads = await _adService.GetAllBySubcategoryAsync(category);
                        break;
                    case "OutdoorPlant":
                        // Retrieve all ads for the "OutdoorPlant" subcategory.
                        _ads = await _adService.GetAllBySubcategoryAsync(category);
                        break;
                    default:
                        // If the category does not match any specific case, retrieve all ads.
                        _ads = await _adService.GetAllAdsAsync();
                        break;
                }
            }
            else
            {
                // If no category is provided, retrieve all ads.
                _ads = await _adService.GetAllAdsAsync();
            }
            // Return the page with the updated data.
            return Page();
        }


        // Method declaration for handling a POST request to sort ads by category
        public async Task<IActionResult> OnPostSortCategoryAsync(string category)
        {
            // Assigning the value of SelectedOption to the local variable 'category'
            category = SelectedOption;

            // Switch statement to handle different values of SelectedOption
            switch (SelectedOption)
            {
                // If SelectedOption is "All" or "Plants", retrieve all ads by the specified category
                case "All":
                    _ads = await _adService.GetAllByCategoryAsync(category);
                    break;
                case "Plants":
                    _ads = await _adService.GetAllByCategoryAsync(category);
                    break;

                // If SelectedOption is "IndoorPlant" or "OutdoorPlant", retrieve ads by subcategory
                case "IndoorPlant":
                    _ads = await _adService.GetAllBySubcategoryAsync(category);
                    break;
                case "OutdoorPlant":
                    _ads = await _adService.GetAllBySubcategoryAsync(category);
                    break;

                // Default case: retrieve all ads if SelectedOption does not match any specified cases
                default:
                    _ads = await _adService.GetAllAdsAsync();
                    break;
            }

            // Return the Razor page after sorting the ads
            return Page();
        }

        // This method is a handler for a POST request related to name search.
        public IActionResult OnPostNameSearch()
        {
            // Call the NameSearch method of the _adService
            // passing the SearchString parameter. Convert the result to a List.
            _ads = _adService.NameSearch(SearchString).ToList();

            // Return the current page. It seems like this method is part of a Razor Page, 
            // so returning Page() will refresh the current page.
            return Page();
        }


        // This method is an event handler for the HTTP POST request made to the server when the price filter form is submitted.
        public IActionResult OnPostPriceFilter()
        {
            // Call the PriceFilter method of the _adService (advertisement service) passing MaxPrice and MinPrice as parameters,
            // then convert the result to a list and assign it to the _ads field.
            _ads = _adService.PriceFilter(MaxPrice, MinPrice).ToList();

            // Return the Razor Page associated with this method.
            return Page();
        }

    }
}
