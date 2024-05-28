
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
        [BindProperty] public IFormFile Photo { get; set; }

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
            // Check if the user is signed in
            if (_signInManager.IsSignedIn(User))
            {
                // If the user is signed in, return the current page
                return Page();
            }

            // If the user is not signed in, redirect to the login page
            // Specify the login page using RedirectToPage method, along with the Identity area
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        public IActionResult OnPost(string category)
        {

            Category = category;
            // Execute method based on the selected category


            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync(string category)
        {
            // Check if the model state is not valid
            if (!ModelState.IsValid)
            {
                // If not valid, return the current page
                return Page();
            }

            // Get the currently logged-in user
            var user = await _userManager.GetUserAsync(User);

            // Switch statement to handle different categories
            switch (category)
            {
                case "IndoorPlant":
                    // Set the user ID for the indoor plant ad
                    IndoorPlantAd.UserId = user.Id;
                    // Add the seller to the database
                    await _sellerService.AddAsync(Seller);
                    // Set the seller ID for the indoor plant ad
                    IndoorPlantAd.SellerId = Seller.Id;

                    // Check if there is a photo uploaded
                    if (Photo != null)
                    {
                        // If there's an existing image, delete it
                        if (IndoorPlantAd.AdImage != null)
                        {
                            string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", IndoorPlantAd.AdImage);
                            System.IO.File.Delete(filepath);
                        }
                    }
                    // Process the uploaded file and set it as the ad image for indoor plant
                    IndoorPlantAd.AdImage = ProcessUploadedFile();

                    // Add the indoor plant ad to the database
                    await _adService.AddAsync(IndoorPlantAd);
                    break;
                case "OutdoorPlant":
                    // Set the user ID for the outdoor plant ad
                    OutdoorPlantAd.UserId = user.Id;
                    // Add the seller to the database
                    await _sellerService.AddAsync(Seller);
                    // Set the seller ID for the outdoor plant ad
                    OutdoorPlantAd.SellerId = Seller.Id;

                    // Check if there is a photo uploaded
                    if (Photo != null)
                    {
                        // If there's an existing image, delete it
                        if (OutdoorPlantAd.AdImage != null)
                        {
                            string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", OutdoorPlantAd.AdImage);
                            System.IO.File.Delete(filepath);
                        }
                    }
                    // Process the uploaded file and set it as the ad image for outdoor plant
                    OutdoorPlantAd.AdImage = ProcessUploadedFile();

                    // Add the outdoor plant ad to the database
                    await _adService.AddAsync(OutdoorPlantAd);
                    break;

            }

            // Redirect to the page that displays all ads after successfully creating the ad
            return RedirectToPage("ShowAllAds");
        }

        // This method processes the uploaded file and saves it to the server.
        private string ProcessUploadedFile()
        {
            string uniqueFileName = Path.Combine(_webHostEnvironment.WebRootPath, "Images\\0ab56030-c9a1-4be5-ba94-eaade74455ff_TurtleBurger.jpg");
            if (Photo != null)
            {
                // Get the path to the "Images" folder within the web root directory.
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                // Generate a unique file name by combining a GUID with the original file name.
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;

                // Combine the uploads folder path with the unique file name to create the full file path.
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Create a file stream to write the uploaded file to disk.
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    // Copy the contents of the uploaded file to the file stream.
                    Photo.CopyTo(fileStream);
                }
            }

            // Return the unique file name, or null if no file was uploaded.
            return uniqueFileName;
        }

    }
}
