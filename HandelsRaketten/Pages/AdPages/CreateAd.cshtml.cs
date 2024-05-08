
using HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HandelsHjornet.Pages.AdPages
{
    public class CreateAdModel : PageModel
    {
        IAdService _adService;

        [BindProperty] public IndoorPlant IndoorPlant { get; set; }
        [BindProperty] public OutdoorPlant OutdoorPlant { get; set; }
        [BindProperty] public Soil Soil { get; set; }
        [BindProperty] public Tool Tool { get; set; }
        [BindProperty] public GardeningTool GardeningTool { get; set; }
        [BindProperty] public Fertilizer Fertilizer { get; set; }


        [BindProperty] public string Category { get; set; }

        public CreateAdModel(IAdService adService)
        {
            _adService = adService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string category)
        {

            Category = category;
            // Execute method based on the selected category


            return Page();
        }

        public IActionResult OnPostCreate(string category)
        {
            if (!string.IsNullOrEmpty(category))
            {
                switch (category)
                {
                    case "IndoorPlant":
                        _adService.Add(IndoorPlant, category);
                        break;
                    case "OutdoorPlant":
                        _adService.Add(OutdoorPlant, category);
                        break;
                    case "Soil":
                        _adService.Add(Soil, category);
                        break;
                    case "Tool":
                        _adService.Add(Tool, category);
                        break;
                    case "GardeningTool":
                        _adService.Add(GardeningTool, category);
                        break;
                    case "Fertilizer":
                        _adService.Add(Fertilizer,category);
                        break;
                    // Add cases for other categories as needed
                    default:
                        // Handle unknown category
                        break;
                }
            }

            return RedirectToPage("ShowAllAds");
        }
    }
}
