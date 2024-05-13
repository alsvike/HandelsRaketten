
using HandelsRaketten.Models;
using HandelsRaketten.Services.DbServices;
using NuGet.Protocol.Plugins;

namespace HandelsRaketten.Services.AdServices
{
    public class SellerService : ISellerService
    {

        IService<Seller> _dbService;

        List<Seller> _sellers;

        public SellerService(IService<Seller> dbService)
        {
            _dbService = dbService;
            _sellers = _dbService.GetObjectsAsync().Result.ToList();
        }

        public async Task<Seller> AddAsync(Seller seller)
        {
            if(seller != null)
            {
                _sellers.Add(seller);
                await _dbService.AddObjectAsync(seller);
                return seller;
            }
            return null;
        }

        public async Task DeleteAsync(Seller seller)
        {
            if(seller != null)
            {
                _sellers.Remove(seller);
                await _dbService.DeleteObjectAsync(seller);
            }

        }

        public Seller Get(int id)
        {
            return _sellers.FirstOrDefault(s => s.Id == id);
        }

        public List<Seller> GetAll()
        {
            return _sellers;
        }

        public async Task UpdateAsync(Seller newSeller, int oldSellerId)
        {
            if(newSeller != null)
            {
                foreach(var seller in _sellers)
                {
                    if(seller.Id == oldSellerId)
                    {
                        seller.Address = newSeller.Address;
                        seller.Email = newSeller.Email;
                        seller.Phone = newSeller.Phone;
                        seller.City = newSeller.City;
                        seller.ZipCode = newSeller.ZipCode;
                        await _dbService.UpdateObjectAsync(seller);
                        break;
                    }
                }
            }
        }
    }
}
