using HandelsRaketten.Models;

namespace HandelsRaketten.Services.AdServices
{
    public interface ISellerService
    {

        Task<Seller> AddAsync(Seller seller);
        Task DeleteAsync(Seller seller);
        Seller Get(int id);
        List<Seller> GetAll();
        Task UpdateAsync(Seller newSeller, int oldSellerId);

    }
}
