
using HandelsRaketten.Areas.Identity.Data;
using HandelsRaketten.Models;
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HandelsHjornet.Pages.AdPages
{
    public class CreateAdModel : PageModel
    {
        IAdService _adService;
        ISellerService _sellerService;
        IWebHostEnvironment _webHostEnvironment;

        SignInManager<User> _signInManager;
        UserManager<User> _userManager;

        [Required(ErrorMessage = "Tilføj Billede")]
        [BindProperty] public IFormFile? Photo { get; set; }

        [BindProperty] public IndoorPlantAd IndoorPlant { get; set; }
        [BindProperty] public OutdoorPlantAd OutdoorPlant { get; set; }

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
            if (!ModelState.IsValid)
            {
                return Page();
            }

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
