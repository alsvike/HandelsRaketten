using HandelsRaketten.Models;
using HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HandelsHjornet.Pages.AdPages
{
    public class ShowAdModel : PageModel
    {
        IAdService _adService;
        ISellerService _sellerService;

        public string Category { get; set; }

        public IndoorPlant IndoorPlant { get; set; }
        public OutdoorPlant OutdoorPlant { get; set; }
        public Soil Soil { get; set; }
        public Tool Tool { get; set; }
        public GardeningTool GardeningTool { get; set; }
        public Fertilizer Fertilizer { get; set; }


        public Seller Seller { get; set; }

        public ShowAdModel(IAdService adService, ISellerService sellerService)
        {
            _adService = adService;
            _sellerService = sellerService;
        }

        public IActionResult OnGet(int adId, string category)
        {

            Category = category;
            switch (category)
            {
                case "IndoorPlant":
                    IndoorPlant = (IndoorPlant)_adService.Get(adId, category);
                    Seller = _sellerService.Get(IndoorPlant.SellerId);
                    break;
                case "OutdoorPlant":
                    OutdoorPlant = (OutdoorPlant)_adService.Get(adId, category);
                    Seller = _sellerService.Get(OutdoorPlant.SellerId);
                    break;
                case "Tool":
                    Tool = (Tool)_adService.Get(adId, category);
                    Seller = _sellerService.Get(Tool.SellerId);
                    break;
                case "GardeningTool":
                    GardeningTool = (GardeningTool)_adService.Get(adId, category);
                    Seller = _sellerService.Get(GardeningTool.SellerId);
                    break;
                case "Fertilizer":
                    Fertilizer = (Fertilizer)_adService.Get(adId, category);
                    Seller = _sellerService.Get(Fertilizer.SellerId);
                    break;
                case "Soil":
                    Soil = (Soil)_adService.Get(adId, category);
                    Seller = _sellerService.Get(Soil.SellerId);
                    break;
                default:
                    return RedirectToPage("/Error");
            }

            return Page();
        }
    }
}
