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
        IMessageService _messageService;
        private readonly UserManager<User> _userManager;
        AdDbContext _context;

        public Ad Ad { get; set; }
        public User CurrentUser { get; set; }
        public IEnumerable<HandelsRaketten.Models.AdModels.Message> Messages { get; set; }

        public AdConversation AdConversation { get; set; }

        [BindProperty] public HandelsRaketten.Models.AdModels.Message NewMessage { get; set; }

        public ShowAdModel(IAdService adService, IMessageService messageService, UserManager<User> userManager, AdDbContext context)
        {
            _context = context;
            _messageService = messageService;
            _adService = adService;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int adId)
        {
            CurrentUser = await _userManager.GetUserAsync(User);
            Ad = await _adService.GetAdConversationAsync(adId);


            if (Ad == null)
            {
                return NotFound();
            }

            AdConversation = _context.AdConversations
                               .Include(c => c.Messages)
                               .ThenInclude(m => m.Sender)
                               .FirstOrDefault(c => c.AdId == Ad.Id && c.SenderId == CurrentUser.Id || c.OwnerId == CurrentUser.Id);

            if(AdConversation == null)
            {
                AdConversation = new AdConversation();
                AdConversation.AdId = Ad.Id;
                AdConversation.OwnerId = Ad.Owner.Id;
                AdConversation.SenderId = CurrentUser.Id;

                _context.AdConversations.Add(AdConversation);
                await _context.SaveChangesAsync();
            }

            Messages = AdConversation.Messages;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int adId)
        {

            CurrentUser = await _userManager.GetUserAsync(User);
            Ad = await _adService.GetAdConversationAsync(adId);

            if (Ad == null)
            {
                return NotFound();
            }

            AdConversation = _context.AdConversations
                   .Include(c => c.Messages)
                   .ThenInclude(m => m.Sender)
                   .FirstOrDefault(c => c.AdId == Ad.Id && c.SenderId == CurrentUser.Id || c.OwnerId == CurrentUser.Id);

            if (AdConversation == null)
            {
                AdConversation = new AdConversation();
                AdConversation.AdId = Ad.Id;
                AdConversation.OwnerId = Ad.Owner.Id;
                AdConversation.SenderId = CurrentUser.Id;

                _context.AdConversations.Add(AdConversation);
                await _context.SaveChangesAsync();
            }

            Messages = AdConversation.Messages;

            if (!ModelState.IsValid)
            {
                return Page();
            }




            NewMessage.AdConversationId = AdConversation.Id;
            NewMessage.Timestamp = DateTime.Now;
            NewMessage.SenderId = CurrentUser.Id;
            _context.Messages.Add(NewMessage);
            _context.SaveChanges();

            return RedirectToPage(new { adId = Ad.Id });
        }
    }
}
