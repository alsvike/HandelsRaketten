using HandelsRaketten.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandelsRaketten.Models.AdModels
{
    [Table("AdConversation")]
    public class AdConversation
    {
        [Key]
        public int Id { get; set; }
        public int AdId { get; set; }
        public string SenderId { get; set; }
        public string OwnerId { get; set; }
        public List<Message> Messages { get; set; }
        public User Sender { get; set; }
        public User Owner { get; set; }
        public Ad Ad { get; set; }

        public AdConversation()
        {

        }
    }
}
