
using HandelsRaketten.Areas.Identity.Data;
using HandelsRaketten.Models;
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HandelsHjornet.Pages.AdPages
{
    public class CreateAdModel : PageModel
    {
        IAdService _adService;
        ISellerService _sellerService;
        IWebHostEnvironment _webHostEnvironment;

        SignInManager<User> _signInManager;
        UserManager<User> _userManager;

        [BindProperty] public IFormFile? Photo { get; set; }

        [BindProperty] public IndoorPlant IndoorPlant { get; set; }
        [BindProperty] public OutdoorPlant OutdoorPlant { get; set; }
        [BindProperty] public Soil Soil { get; set; }
        [BindProperty] public Tool Tool { get; set; }
        [BindProperty] public GardeningTool GardeningTool { get; set; }
        [BindProperty] public Fertilizer Fertilizer { get; set; }

        // information about the user that is to be displayed on the ShowAd page
        [BindProperty] public Seller Seller { get; set; }

        [BindProperty] public string Category { get; set; }

        public CreateAdModel(IAdService adService, ISellerService sellerService, IWebHostEnvironment webHostEnvironment, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _adService = adService;
            _sellerService = sellerService;
            _webHostEnvironment = webHostEnvironment;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return Page();
            }

            // this should route to the login side if the user is not logged in
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        public IActionResult OnPost(string category)
        {

            Category = category;
            // Execute method based on the selected category


            return Page();
        }

        public async Task<IActionResult> OnPostCreate(string category)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            var createdAd = await CreateAdAsync(category);

            if(createdAd == null)
            {
                return Page();
            }

            return RedirectToPage("ShowAllAds");
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                { Photo.CopyTo(fileStream); }
            }
            return uniqueFileName;
        }

        private async Task<Ad> CreateAdAsync(string category)
        {
            var userId = _userManager.GetUserId(User);

            if (!string.IsNullOrEmpty(category))
            {
                switch (category)
                {
                    case "IndoorPlant":
                        var indoorPlantSeller = await _sellerService.AddAsync(Seller);
                        IndoorPlant.SellerId = indoorPlantSeller.Id;
                        IndoorPlant.UserId = userId;

                        if (Photo != null)
                        {
                            if (IndoorPlant.AdImage != null)
                            {
                                string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", IndoorPlant.AdImage);
                                System.IO.File.Delete(filepath);
                            }
                        }
                        IndoorPlant.AdImage = ProcessUploadedFile();

                        return await _adService.AddAsync(IndoorPlant, category);


                    case "OutdoorPlant":
                        var outdoorPlantSeller = await _sellerService.AddAsync(Seller);
                        OutdoorPlant.SellerId = outdoorPlantSeller.Id;
                        OutdoorPlant.UserId = userId;

                        if (Photo != null)
                        {
                            if (OutdoorPlant.AdImage != null)
                            {
                                string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", OutdoorPlant.AdImage);
                                System.IO.File.Delete(filepath);
                            }
                        }
                        OutdoorPlant.AdImage = ProcessUploadedFile();

                        return await _adService.AddAsync(OutdoorPlant, category);


                    case "Soil":
                        var soilSeller = await _sellerService.AddAsync(Seller);
                        Soil.SellerId = soilSeller.Id;
                        Soil.UserId = userId;

                        if (Photo != null)
                        {
                            if (Soil.AdImage != null)
                            {
                                string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", Soil.AdImage);
                                System.IO.File.Delete(filepath);
                            }
                        }
                        Soil.AdImage = ProcessUploadedFile();

                        return await _adService.AddAsync(Soil, category);


                    case "Tool":
                        var toolSeller = await _sellerService.AddAsync(Seller);
                        Tool.SellerId = toolSeller.Id;
                        Tool.UserId = userId;

                        if (Photo != null)
                        {
                            if (Tool.AdImage != null)
                            {
                                string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", Tool.AdImage);
                                System.IO.File.Delete(filepath);
                            }
                        }
                        Tool.AdImage = ProcessUploadedFile();

                        return await _adService.AddAsync(Tool, category);


                    case "GardeningTool":
                        var gardeningToolSeller = await _sellerService.AddAsync(Seller);
                        GardeningTool.SellerId = gardeningToolSeller.Id;
                        GardeningTool.UserId = userId;

                        if (Photo != null)
                        {
                            if (GardeningTool.AdImage != null)
                            {
                                string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", GardeningTool.AdImage);
                                System.IO.File.Delete(filepath);
                            }
                        }
                        GardeningTool.AdImage = ProcessUploadedFile();

                        return await _adService.AddAsync(GardeningTool, category);

                    case "Fertilizer":
                        var fertilizerSeller = await _sellerService.AddAsync(Seller);
                        Fertilizer.SellerId = fertilizerSeller.Id;
                        Fertilizer.UserId = userId;

                        if (Photo != null)
                        {
                            if (Fertilizer.AdImage != null)
                            {
                                string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", Fertilizer.AdImage);
                                System.IO.File.Delete(filepath);
                            }
                        }
                        Fertilizer.AdImage = ProcessUploadedFile();

                        return await _adService.AddAsync(Fertilizer, category);


                    // Add more categories
                    default:
                        // Handle unknown category
                        break;
                }
            }
            return null;
        }

    }
}
