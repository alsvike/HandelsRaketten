﻿using HandelsRaketten.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandelsRaketten.Models.AdModels
{
    [Table("Message")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? SenderId { get; set; }
        public int? AdConversationId { get; set; }
        public User? Sender { get; set; }
        public AdConversation? AdConversation { get; set; }


        public Message()
        {
        }
    }
}
