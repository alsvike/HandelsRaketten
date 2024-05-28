
using HandelsRaketten.Areas.Identity.Data;
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HandelsHjornet.Pages.TestPages
{
    public class ViewCategoriesTestPageModel : PageModel
    {
        IAdService _adService;

        UserManager<User> _userManager;

        [BindProperty]
        public string SelectedOption { get; set; }

        [BindProperty]
        public string? SearchString { get; set; }

        [BindProperty]
        public int MinPrice { get; set; }

        [BindProperty]
        public int MaxPrice { get; set; }

        public List<Ad> Ads { get; set; }


        bool _isNameSorted;
        bool _isPriceSorted;
        public ViewCategoriesTestPageModel(IAdService adService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _adService = adService;
        }

        // This method is an asynchronous handler for HTTP GET requests.
        public async Task<IActionResult> OnGetAsync()
        {
            // Calls the GetAllAdsAsync method of the _adService to retrieve all ads asynchronously.
            Ads = await _adService.GetAllAdsAsync();

            // Retrieves the currently logged-in user asynchronously.
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return RedirectToPage("/Index");
            }
            if (!user.IsAdmin)
            {
                // Redirects the user to the Index page if they are not an administrator.
                return RedirectToPage("/Index");
            }

            // Returns the Razor Page if the user is an administrator.
            return Page();
        }


        // This method handles HTTP POST requests sent to the current page.
        public async Task<IActionResult> OnPostAsync()
        {
            // Checks if the model state is valid (i.e., if the data passed validation rules).
            if (!ModelState.IsValid)
            {
                // Returns the current page if the model state is not valid.
                return Page();
            }

            // Calls the asynchronous method to change the category based on the selected option.
            await ChangeCategoryAsync(SelectedOption);

            // Returns the current page after handling the POST request.
            return Page();
        }

        private async Task ChangeCategoryAsync(string selectedOption)
        {
            // Switch statement to handle different category options
            switch (selectedOption)
            {
                // If selectedOption is "All", fetch all ads regardless of category
                case "All":
                    Ads = await _adService.GetAllByCategoryAsync(selectedOption);
                    break;
                // If selectedOption is "Plants", fetch ads for Plants category
                case "Plants":
                    Ads = await _adService.GetAllByCategoryAsync(selectedOption);
                    break;
                // If selectedOption is "IndoorPlant" or "OutdoorPlant", fetch ads for the respective subcategory
                case "IndoorPlant":
                case "OutdoorPlant":
                    Ads = await _adService.GetAllBySubcategoryAsync(selectedOption);
                    break;
                // If selectedOption is any other category of tools or accessories, fetch ads for the respective subcategory
                case "Tool":
                case "GardeningTool":
                case "PlantAccessories":
                case "Soil":
                case "Fertilizer":
                    Ads = await _adService.GetAllBySubcategoryAsync(selectedOption);
                    break;
                // If selectedOption does not match any specific category, fetch all ads
                default:
                    Ads = await _adService.GetAllAdsAsync();
                    break;
            }
        }

        // This method is called when the page is requested to sort the ads by name.
        public IActionResult OnGetSortName()
        {
            // Checking if TempData contains the key "IsNameSorted" to determine if the names are already sorted.
            if (TempData.ContainsKey("IsNameSorted"))
            {
                // If the key exists, retrieve the stored boolean value indicating whether the names are sorted.
                _isNameSorted = (bool)TempData["IsNameSorted"];
            }

            // Sorting logic based on the current state of name sorting.
            if (_isNameSorted)
            {
                // If names are already sorted in descending order, sort them in ascending order and update the flag.
                Ads = _adService.SortByNameDescending().ToList();
                _isNameSorted = false;
            }
            else
            {
                // If names are not sorted or sorted in ascending order, sort them in descending order and update the flag.
                Ads = _adService.SortByName().ToList();
                _isNameSorted = true;
            }

            // Storing the updated sorting state (sorted or not) in TempData for future requests.
            TempData["IsNameSorted"] = _isNameSorted;

            // Returning the page after sorting the ads.
            return Page();
        }


        // This method is an action method called when the page is requested to be sorted by price.
        public IActionResult OnGetSortPrice()
        {
            // Check if TempData contains a key indicating whether the ads are already sorted by price.
            if (TempData.ContainsKey("IsPriceSorted"))
            {
                // If the key exists, retrieve the value and cast it to a boolean.
                _isPriceSorted = (bool)TempData["IsPriceSorted"];
            }

            // Check if the ads are already sorted by price.
            if (_isPriceSorted)
            {
                // If ads are already sorted by price in descending order, 
                // retrieve the sorted list and convert it to a list.
                Ads = _adService.SortByPriceDescending().ToList();
                // Update the sorting status to false, indicating that the next sorting will be ascending.
                _isPriceSorted = false;
            }
            else
            {
                // If ads are not already sorted by price, 
                // retrieve the sorted list in ascending order and convert it to a list.
                Ads = _adService.SortByPrice().ToList();
                // Update the sorting status to true, indicating that the next sorting will be descending.
                _isPriceSorted = true;
            }

            // Store the updated sorting status in TempData to maintain it across requests.
            TempData["IsPriceSorted"] = _isPriceSorted;

            // Return the page with the sorted ads.
            return Page();
        }


        // This method handles the POST request for name search functionality.
        public IActionResult OnPostNameSearch()
        {
            // Calls the NameSearch method of the injected _adService with the SearchString parameter
            Ads = _adService.NameSearch(SearchString).ToList();
            // Returns the current page
            return Page();
        }

        // This method is executed when the user submits a form with price filter parameters
        public IActionResult OnPostPriceFilter()
        {
            // Filtering ads based on price range using the AdService PriceFilter method
            Ads = _adService.PriceFilter(MaxPrice, MinPrice).ToList();

            // Returning the current page after applying the price filter
            return Page();
        }

    }
}
