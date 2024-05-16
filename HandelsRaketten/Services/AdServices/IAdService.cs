using HandelsRaketten.Models.AdModels;

namespace HandelsRaketten.Services.AdServices
{
    public interface IAdService
    {
        Task AddAsync(Ad ad);
        Task<Ad> DeleteAsync(int adId);
        Ad Get(int adId);
        Task UpdateAsync(int adId, Ad ad);
        Task<List<Ad>> GetAllByCategoryAsync(string category);
        Task<List<Ad>> GetAllBySubcategoryAsync(string discriminator);

        Task<List<Ad>> GetAllAdsAsync();
        Task<Ad> GetAdConversationAsync(int id);

        IEnumerable<Ad> NameSearch(string str);
        IEnumerable<Ad> PriceFilter(int maxPrice, int minPrice = 0);
        IEnumerable<Ad> SortByPrice();
        IEnumerable<Ad> SortByPriceDescending();
        IEnumerable<Ad> SortByName();
        IEnumerable<Ad> SortByNameDescending();



    }
}
