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
        public User? CurrentUser { get; set; }
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

        // This method handles HTTP GET requests asynchronously, taking an adId parameter
        public async Task<IActionResult> OnGetAsync(int adId)
        {
            // Check if the provided adId is negative. If so, return a BadRequest response.
            if (adId < 0)
            {
                return BadRequest("Annonce id m� ikke v�re negativ");
            }

            CurrentUser = await _userManager.GetUserAsync(User);
            Ad = await _adService.GetAdConversationAsync(adId);


            if (Ad == null)
            {
                return NotFound("Annonce findes ikke");
            }

            if(CurrentUser != null)
            {
                if (CurrentUser.Id != Ad.Owner.Id)
                    await GetAdConversationAsync();

                if (CurrentUser.Id == Ad.Owner.Id)
                    await GetAdConversationsAsync();
            }


            if(AdConversation != null && CurrentUser.Id != Ad.Owner.Id)
            Messages = AdConversation.Messages;

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
                    await GetAdConversationAsync();

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
            else if (CurrentUser != null && CurrentUser.Id == Ad.Owner.Id)
            {
                if (AdConversations == null)
                {
                    await GetAdConversationsAsync();
                }

                if (AdConversations != null)
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
                string errorMessage = "Du skal v�re logget ind for at skrive en besked.";

                // Add message to ModelState
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteMessageAsync(int adId, int msgId)
        {
            CurrentUser = await _userManager.GetUserAsync(User);
            Ad = await _adService.GetAdConversationAsync(adId);

            if(Ad == null)
            {
                return NotFound("Ad was Null");
            }

            if(CurrentUser != null)
            {
                await _adService.DeleteMessageAsync(msgId);
            }

            return RedirectToPage("./ShowAd", new { adId = Ad.Id });
        }

        // Method to asynchronously retrieve an ad conversation
        private async Task<AdConversation> GetAdConversationAsync()
        {
            // Check if there is a current user
            if (CurrentUser != null)
            {
                    // Retrieve the ad conversation from the database context
                    // Including messages and their senders
                    AdConversation = _context.AdConversations
                       .Include(c => c.Messages)
                       .ThenInclude(m => m.Sender)
                       // Find the first ad conversation matching the conditions:
                       // AdId matches the current ad's Id, and either the sender Id or the owner Id matches the current user's Id
                       .FirstOrDefault(c => c.AdId == Ad.Id && (c.SenderId == CurrentUser.Id || c.OwnerId == CurrentUser.Id));
            }
            // Return the default value (null for reference types)
            return default;
        }


        private async Task<AdConversation> GetAdConversationsAsync()
        {
            // Check if there is a current user
            if (CurrentUser != null)
            {
                    // Retrieve ad conversations from the database context
                    AdConversations = _context.AdConversations
                       // Include messages related to each conversation
                       .Include(c => c.Messages)
                       // Then include the sender of each message
                       .ThenInclude(m => m.Sender)
                       // Filter conversations based on ad ID and sender or owner ID matching the current user
                       .Where(c => c.AdId == Ad.Id && (c.SenderId == CurrentUser.Id || c.OwnerId == CurrentUser.Id)).ToList();
            }
            // Return default value (null in this case)
            return default;
        }
    }
}
