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

namespace HandelsHjornet.Pages.AdPages
{
    public class ShowAdModel : PageModel
    {
        IAdService _adService;
        IMessageService _messageService;
        private readonly UserManager<User> _userManager;


        public Ad Ad { get; set; }
        public User CurrentUser { get; set; }
        public IEnumerable<Message> Messages { get; set; }

        [BindProperty] public Message NewMessage { get; set; }

        public ShowAdModel(IAdService adService, IMessageService messageService, UserManager<User> userManager)
        {
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

            if (Ad.Owner.Id != CurrentUser.Id)
            {
                var msg = Ad.Messages.Where(m => m.Sender.Id == CurrentUser.Id && m.AdId == Ad.Id);
                Messages = msg;
            } 
            else if(Ad.Owner.Id == CurrentUser.Id)
            {
                var msg = Ad.Messages;
                Messages = msg;
            }


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

            Messages = Ad.Messages.Where(m => m.Sender.Id == CurrentUser.Id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            NewMessage.AdId = Ad.Id;
            NewMessage.SenderId = CurrentUser.Id;
            NewMessage.Timestamp = DateTime.Now;
              


            await _messageService.AddAsync(NewMessage);


            return RedirectToPage(new { adId = Ad.Id });
        }
    }
}
