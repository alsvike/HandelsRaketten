using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Services.DbServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HandelsRaketten.Services.AdServices
{
    //public class MessageService : IMessageService
    //{
    //    IService<Message> _dbService;
    //    List<Message> _messages;

    //    public MessageService(IService<Message> dbService)
    //    {
    //        _dbService = dbService;
    //        _messages = _dbService.GetObjectsAsync().Result.ToList();
    //    }

    //    public async Task<Message> AddAsync(Message message)
    //    {
    //        if(message != null)
    //        {
    //            _messages.Add(message);
    //            await _dbService.AddObjectAsync(message);
    //            return message;

    //        }
    //        return null;
    //    }

    //    public async Task DeleteAsync(Message message)
    //    {
    //        if(message != null)
    //        {
    //            _messages.Remove(message);
    //            await _dbService.DeleteObjectAsync(message);
    //        }
    //    }

    //    public Message Get(int id) => _messages.FirstOrDefault(m => m.Id == id);

    //    public List<Message> GetAll() => _messages;

    //    public async Task UpdateAsync(Message newMessage, int oldMessageId)
    //    {
    //        if(newMessage != null)
    //        {
    //            var oldM = Get(oldMessageId);

    //            oldM.Content = newMessage.Content;
    //            await _dbService.UpdateObjectAsync(oldM);
    //        }
    //    }
    //}
}
