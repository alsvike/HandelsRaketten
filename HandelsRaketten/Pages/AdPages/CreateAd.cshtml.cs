
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

        [BindProperty]
        public IndoorPlantAd IndoorPlantAd { get; set; }

        [BindProperty]
        public OutdoorPlantAd OutdoorPlantAd { get; set; }

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
            // check if user is signed in
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

            return Page();
        }

        public async Task<IActionResult> OnPostCreate(string category)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await CreateAd(category);

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

        private async Task CreateAd(string category)
        {
            var user = await _userManager.GetUserAsync(User);
            switch (category)
            {
                case "IndoorPlant":
                    
                    if (Photo != null)
                    {
                        IndoorPlantAd.Owner = user;
                        IndoorPlantAd.UserId = user.Id;
                        await _sellerService.AddAsync(Seller);
                        IndoorPlantAd.Seller = Seller;
                        IndoorPlantAd.SellerId = Seller.Id;
                        if (IndoorPlantAd.AdImage != null)
                        {
                            string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", IndoorPlantAd.AdImage);
                            System.IO.File.Delete(filepath);
                        }
                    }
                    IndoorPlantAd.AdImage = ProcessUploadedFile();

                    await _adService.AddAsync(IndoorPlantAd);
                    break;
                case "OutdoorPlant":
                    OutdoorPlantAd.Owner = user;
                    OutdoorPlantAd.UserId = user.Id;
                    await _sellerService.AddAsync(Seller);
                    OutdoorPlantAd.Seller = Seller;
                    OutdoorPlantAd.SellerId = Seller.Id;
                    if (Photo != null)
                    {
                        if (OutdoorPlantAd.AdImage != null)
                        {
                            string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", OutdoorPlantAd.AdImage);
                            System.IO.File.Delete(filepath);
                        }
                    }
                    OutdoorPlantAd.AdImage = ProcessUploadedFile();

                    await _adService.AddAsync(OutdoorPlantAd);
                    break;
            }
        }
    }
}
