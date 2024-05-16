using HandelsRaketten.Areas.Identity.Data;

namespace HandelsRaketten.Models.AdModels
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string SenderId { get; set; }
        public User Sender { get; set; }
        public int AdId { get; set; }
        public Ad Ad { get; set; }
    }
}
