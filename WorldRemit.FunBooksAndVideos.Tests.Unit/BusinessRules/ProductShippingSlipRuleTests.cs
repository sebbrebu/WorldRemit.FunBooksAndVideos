using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using WorldRemit.FunBooksAndVideos.Api.BusinessRules;
using WorldRemit.FunBooksAndVideos.Api.Models;
using WorldRemit.FunBooksAndVideos.Api.Services;
using Xunit;

namespace WorldRemit.FunBooksAndVideos.Tests.Unit.BusinessRules
{
    public class ProductShippingSlipRuleTests
    {
        private readonly Mock<IShippingService> _mockShippingService;
        private readonly ProductShippingSlipRule _productShippingSlipRule;

        public ProductShippingSlipRuleTests()
        {
            _mockShippingService = new Mock<IShippingService>();
            _productShippingSlipRule = new ProductShippingSlipRule(_mockShippingService.Object);
        }

        [Fact]
        public async Task Apply_CallsGenerateShippingSlipOnce_WithCorrectParameters()
        {
            //arrange
            var items = new List<Item>()
                {
                    new Item { Id = 10, Name = "Book", Type = ItemType.Product },
                    new Item { Id = 16, Name = "Another book", Type = ItemType.Product },
                    new Item { Id = 17, Name = "Gold Membership", Type = ItemType.Membership }
                };

            var order = new PurchaseOrder
            {
                Id = 1,
                CustomerId = 3,
                Total = 23.5m,
                Items = items
            };

            //act      
            await _productShippingSlipRule.ApplyAsync(order);

            //assert
            _mockShippingService.Verify(x => x.GenerateShippingSlip(3, new List<Item>() { items[0], items[1] }), Times.Once());
        }

        [Fact]
        public async Task Apply_DoesNotCallGenerateShippingSlip_WhenNoProductItems()
        {
            //arrange
            var order = new PurchaseOrder
            {
                Id = 1,
                CustomerId = 3,
                Total = 23.5m,
                Items = new List<Item>()
                    {
                        new Item { Id = 17, Name = "Gold Membership", Type = ItemType.Membership }
                    }
            };

            //act      
            await _productShippingSlipRule.ApplyAsync(order);

            //assert
            _mockShippingService.Verify(x => x.GenerateShippingSlip(It.IsAny<int>(), It.IsAny<IEnumerable<Item>>()), Times.Never());
        }
    }
}