﻿using HandelsRaketten.Models.AdModels;

namespace HandelsRaketten.Services.DbServices
{
    public interface IAdDbService
    {
        Task<IEnumerable<Ad>> GetObjectsAsync();
        Task AddObjectAsync(Ad obj);
        Task DeleteObjectAsync(Ad obj);
        Task UpdateObjectAsync(Ad obj);
        Task<Ad> GetObjectByIdAsync(int id);
        Task<List<Ad>> GetAllByUserIdAsync(string userId);
        Task SaveObjectsAsync(List<Ad> objs);

        Task DeleteMessageAsync(int msgId);
        Task AddMessageAsync(Message message);
        Task<Ad> GetAdConversationAsync(int id);

        Task<List<Ad>> GetAllByCategoryAsync(string category);
        Task<List<Ad>> GetAllBySubcategoryAsync(string dicriminator);




    }
}
