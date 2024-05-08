using HandelsRaketten.Models.AdModels;

namespace HandelsRaketten.Services.AdServices
{
    public interface IAdService
    {
        Ad Add(Ad ad, string category);
        Ad Delete(int adId, string category);
        Ad Get(int adId, string category);
        void Edit(int adId, Ad ad, string category);
        List<Ad> GetAll(string category);
        List<Ad> GetAllAds();

    }
}
