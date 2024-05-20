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

        public ShowAdModel(IAdService adService, UserManager<User> userManager, AdDbContext context)
        {
            _context = context;
            _adService = adService;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int adId)
        {
            if (adId <= 0)
            {
                return BadRequest("Invalid Ad ID.");
            }

            CurrentUser = await _userManager.GetUserAsync(User);
            Ad = await _adService.GetAdConversationAsync(adId);


            if (Ad == null)
            {
                return NotFound();
            }

            if (_context == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }


            await GetAdConversation();

            if(AdConversation == null && CurrentUser != null)
            {
                AdConversation = new AdConversation();
                AdConversation.AdId = Ad.Id;
                AdConversation.OwnerId = Ad.Owner.Id;
                AdConversation.SenderId = CurrentUser.Id;

                _context.AdConversations.Add(AdConversation);
                await _context.SaveChangesAsync();
            }

            if(AdConversation != null)
            Messages = AdConversation.Messages;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int adId)
        {
            // Check if CurrentUser is logged in
            CurrentUser = await _userManager.GetUserAsync(User);
            Ad = await _adService.GetAdConversationAsync(adId);
            if (CurrentUser != null)
            {
                // Ensure AdConversation is loaded
                if (AdConversation == null)
                {
                    AdConversation = await _context.AdConversations
                                                   .Include(c => c.Messages)
                                                       .ThenInclude(m => m.Sender)
                                                   .FirstOrDefaultAsync(c => c.AdId == Ad.Id &&
                                                                             (c.SenderId == CurrentUser.Id || c.OwnerId == CurrentUser.Id));
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
                    return NotFound("Conversation not found.");
                }
            }
            else
            {
                // Show message that says you must be logged in
                string errorMessage = "You must be logged in to send a message.";

                // Show message that says you must be logged in
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
    }
}
