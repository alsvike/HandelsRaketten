using HandelsRaketten.Models.AdModels;

namespace HandelsRaketten.Services.AdServices
{
    public interface IAdService
    {
        Task<Ad> AddAsync(Ad ad, string category);
        Task<Ad> DeleteAsync(int adId, string category);
        Ad Get(int adId, string category);
        Task UpdateAsync(int adId, Ad ad, string category);
        List<Ad> GetAll(string category);
        List<Ad> GetAllAds();

        IEnumerable<Ad> NameSearch(string str);
        IEnumerable<Ad> PriceFilter(int maxPrice, int minPrice = 0);
        IEnumerable<Ad> SortByPrice();
        IEnumerable<Ad> SortByPriceDescending();
        IEnumerable<Ad> SortByName();
        IEnumerable<Ad> SortByNameDescending();
    }
}
