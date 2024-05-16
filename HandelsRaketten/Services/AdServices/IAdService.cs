using HandelsRaketten.Models.AdModels;

namespace HandelsRaketten.Services.AdServices
{
    public interface IAdService
    {
        Task<Ad> AddAsync(Ad ad, string category);
        Task<Ad> DeleteAsync(int adId, string category);
        Ad Get(int adId, string category);
        Task UpdateAsync(int adId, Ad ad, string category);
        Task<List<Ad>> GetAllAsync(string category);
        Task<List<Ad>> GetAllAdsAsync();
        Task<Ad> GetAdConversationAsync(int id, string category);

        IEnumerable<Ad> NameSearch(string str);
        IEnumerable<Ad> PriceFilter(int maxPrice, int minPrice = 0);
        IEnumerable<Ad> SortByPrice();
        IEnumerable<Ad> SortByPriceDescending();
        IEnumerable<Ad> SortByName();
        IEnumerable<Ad> SortByNameDescending();


    }
}
