using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandelsRaketten.Models;
using HandelsRaketten.Services.AdServices;
using HandelsRaketten.Services.DbServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HandelsRaketten.Tests.Services
{
    [TestClass]
    public class SellerServiceTests
    {
        [TestMethod]
        public async Task AddAsync_ValidSeller_ReturnsAddedSeller()
        {
            // Arrange
            var mockDbService = new Mock<IService<Seller>>();
            var sellerService = new SellerService(mockDbService.Object);
            var seller = new Seller { Id = 1 };

            // Act
            var addedSeller = await sellerService.AddAsync(seller);

            // Assert
            Assert.IsNotNull(addedSeller);
            Assert.AreEqual(seller, addedSeller);
            mockDbService.Verify(m => m.AddObjectAsync(seller), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ValidSeller_RemovesFromListAndCallsDbService()
        {
            // Arrange
            var seller = new Seller { Id = 1 };
            var mockDbService = new Mock<IService<Seller>>();
            var sellerService = new SellerService(mockDbService.Object);
            await sellerService.AddAsync(seller);

            // Act
            await sellerService.DeleteAsync(seller);

            // Assert
            CollectionAssert.AreEqual(new List<Seller>(), sellerService.GetAll());
            mockDbService.Verify(m => m.DeleteObjectAsync(seller), Times.Once);
        }

        [TestMethod]
        public void Get_ValidId_ReturnsSeller()
        {
            // Arrange
            var seller = new Seller { Id = 1 };
            var mockDbService = new Mock<IService<Seller>>();
            var sellerService = new SellerService(mockDbService.Object);
            sellerService.AddAsync(seller).Wait();

            // Act
            var retrievedSeller = sellerService.Get(1);

            // Assert
            Assert.AreEqual(seller, retrievedSeller);
        }

        [TestMethod]
        public void GetAll_ReturnsAllSellers()
        {
            // Arrange
            var sellers = new List<Seller>
            {
                new Seller { Id = 1 },
                new Seller { Id = 2 }
            };
            var mockDbService = new Mock<IService<Seller>>();
            var sellerService = new SellerService(mockDbService.Object);
            foreach (var seller in sellers)
            {
                sellerService.AddAsync(seller).Wait();
            }

            // Act
            var retrievedSellers = sellerService.GetAll();

            // Assert
            CollectionAssert.AreEqual(sellers, retrievedSellers.ToList());
        }

        [TestMethod]
        public async Task UpdateAsync_ValidSeller_UpdatesAndCallsDbService()
        {
            // Arrange
            var oldSellerId = 1;
            var newSeller = new Seller
            {
                Id = 1,
                Address = "Updated Address",
                Email = "updated@example.com",
                Phone = "123456789",
                City = "Updated City",
                ZipCode = 12345
            };
            var sellers = new List<Seller>
            {
                new Seller { Id = 1 }
            };
            var mockDbService = new Mock<IService<Seller>>();
            var sellerService = new SellerService(mockDbService.Object);
            foreach (var seller in sellers)
            {
                await sellerService.AddAsync(seller);
            }

            // Act
            await sellerService.UpdateAsync(newSeller, oldSellerId);

            // Assert
            var updatedSeller = sellerService.Get(oldSellerId);
            Assert.IsNotNull(updatedSeller);
            Assert.AreEqual(newSeller.Address, updatedSeller.Address);
            Assert.AreEqual(newSeller.Email, updatedSeller.Email);
            Assert.AreEqual(newSeller.Phone, updatedSeller.Phone);
            Assert.AreEqual(newSeller.City, updatedSeller.City);
            Assert.AreEqual(newSeller.ZipCode, updatedSeller.ZipCode);
            mockDbService.Verify(m => m.UpdateObjectAsync(It.IsAny<Seller>()), Times.Once);
        }
    }
}
