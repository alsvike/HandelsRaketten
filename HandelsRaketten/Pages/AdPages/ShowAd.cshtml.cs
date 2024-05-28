using HandelsRaketten.Areas.Identity.Data;
using HandelsRaketten.Data;
using HandelsRaketten.EFDBContext;
using HandelsRaketten.Models;
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using HandelsRaketten.Services.AdServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace HandelsHjornet.Pages.AdPages
{
    public class ShowAdModel : PageModel
    {
        IAdService _adService;
        //IMessageService _messageService;
        private readonly UserManager<User> _userManager;
        AdDbContext _context;

        public Ad Ad { get; set; }
        public User CurrentUser { get; set; }
        public IEnumerable<HandelsRaketten.Models.AdModels.Message>? Messages { get; set; }

        public AdConversation? AdConversation { get; set; }

        [BindProperty] public HandelsRaketten.Models.AdModels.Message NewMessage { get; set; }
        public List<AdConversation>? AdConversations { get; set; }

        public ShowAdModel(IAdService adService, UserManager<User> userManager, AdDbContext context)
        {
            _context = context;
            _adService = adService;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int adId)
        {
            // Check if the provided adId is negative. If so, return a BadRequest response.
            if (adId < 0)
            {
                return BadRequest("Annonce id må ikke være negativ");
            }

            // Get the current user asynchronously using the _userManager service.
            CurrentUser = await _userManager.GetUserAsync(User);

            // Retrieve the ad conversation asynchronously using the provided adId from the _adService.
            Ad = await _adService.GetAdConversationAsync(adId);

            // If the ad does not exist, return a NotFound response.
            if (Ad == null)
            {
                return NotFound("Annonce findes ikke");
            }

            if(CurrentUser != null)
            {
                // If the current user is not the owner of the ad, get the ad conversation for that user.
                if (CurrentUser.Id != Ad.Owner.Id)
                {
                    await GetAdConversation();
                }

                // If the current user is the owner of the ad, get all ad conversations.
                if (CurrentUser.Id == Ad.Owner.Id)
                {
                    await GetAdConversations();
                }
            }

            // If there is an ad conversation and the current user is not the owner, set the Messages property.
            if (AdConversation != null && CurrentUser.Id != Ad.Owner.Id)
            {
                Messages = AdConversation.Messages;
            }

            // Return the current page.
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int adId, int AdConversationId)
        {
            // Check if CurrentUser is logged in
            CurrentUser = await _userManager.GetUserAsync(User);
            Ad = await _adService.GetAdConversationAsync(adId);
            if (CurrentUser != null && CurrentUser.Id != Ad.Owner.Id)
            {
                // Ensure AdConversation is loaded
                if (AdConversation == null)
                {
                    // If a AdConversation exists load it
                    await GetAdConversation();

                    // If it doesn't exist create a new
                    if (AdConversation == null && CurrentUser != null && CurrentUser.Id != Ad.Owner.Id)
                    {
                        AdConversation = new AdConversation();
                        AdConversation.AdId = Ad.Id;
                        AdConversation.OwnerId = Ad.Owner.Id;
                        AdConversation.SenderId = CurrentUser.Id;

                        _context.AdConversations.Add(AdConversation);
                        await _context.SaveChangesAsync();
                    }
                }

                // Check if AdConversation is found
                if (AdConversation != null)
                {
                    // Set properties of the new message
                    NewMessage.AdConversationId = AdConversation.Id;
                    NewMessage.Timestamp = DateTime.Now;
                    NewMessage.SenderId = CurrentUser.Id;

                    // Add the new message to the context
                    _context.Messages.Add(NewMessage);
                    await _context.SaveChangesAsync();

                    // Redirect or return a success message/page
                    return RedirectToPage("./ShowAd", new { adId = Ad.Id });
                }
                else
                {
                    return NotFound("Samtale eksisterer ikke.");
                }
            }
            else if(CurrentUser != null && CurrentUser.Id == Ad.Owner.Id)
            {
                if(AdConversations == null)
                {
                    await GetAdConversations();
                }

                if(AdConversations != null)
                {
                    // Set properties of the new message
                    NewMessage.AdConversationId = AdConversationId;
                    NewMessage.Timestamp = DateTime.Now;
                    NewMessage.SenderId = CurrentUser.Id;

                    // Add the new message to the context
                    _context.Messages.Add(NewMessage);
                    await _context.SaveChangesAsync();

                    // Redirect or return a success message/page
                    return RedirectToPage("./ShowAd", new { adId = Ad.Id });
                }
                else
                {
                    return NotFound("Samtale eksisterer ikke.");
                }
            }
            else
            {
                // Show message that says you must be logged in
                string errorMessage = "Du skal være logget ind for at skrive en besked.";

                // Add message to ModelState
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
                return Page();
            }
        }


        private async Task<AdConversation> GetAdConversation()
        {
            if (CurrentUser != null)
            {
                try
                {
                    AdConversation = _context.AdConversations
                       .Include(c => c.Messages)
                       .ThenInclude(m => m.Sender)
                       .FirstOrDefault(c => c.AdId == Ad.Id && (c.SenderId == CurrentUser.Id || c.OwnerId == CurrentUser.Id));
                }
                catch (Exception e)
                {
                    await Console.Out.WriteLineAsync();
                }
            }
            return default;
        }

        private async Task<AdConversation> GetAdConversations()
        {
            if (CurrentUser != null)
            {
                try
                {
                    AdConversations = _context.AdConversations
                       .Include(c => c.Messages)
                       .ThenInclude(m => m.Sender)
                       .Where(c => c.AdId == Ad.Id && (c.SenderId == CurrentUser.Id || c.OwnerId == CurrentUser.Id)).ToList();
                }
                catch (Exception e)
                {
                    await Console.Out.WriteLineAsync();
                }
            }
            return default;
        }
    }
}
