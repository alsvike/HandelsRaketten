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
        Task SaveObjects(List<Ad> objs);

        Task<Ad> GetAdConversationAsync(int id, string category);
    }
}