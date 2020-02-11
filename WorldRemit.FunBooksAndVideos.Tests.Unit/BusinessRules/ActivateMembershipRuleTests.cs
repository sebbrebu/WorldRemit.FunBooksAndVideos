using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using WorldRemit.FunBooksAndVideos.Api.BusinessRules;
using WorldRemit.FunBooksAndVideos.Api.Models;
using WorldRemit.FunBooksAndVideos.Api.Services;
using Xunit;

namespace WorldRemit.FunBooksAndVideos.Tests.Unit.BusinessRules
{
    public class ActivateMembershipRuleTests
    {
        private readonly Mock<ICustomerAccountService> _mockCustomerAccountService;
        private readonly ActivateMembershipRule _activateMembershipRule;

        public ActivateMembershipRuleTests()
        {
            _mockCustomerAccountService = new Mock<ICustomerAccountService>();
            _activateMembershipRule = new ActivateMembershipRule(_mockCustomerAccountService.Object);
        }

        [Fact]
        public async Task Apply_CallsActivateMembershipOnce_ForEachMembershipItem()
        {
            //arrange
            var order = new PurchaseOrder
            {
                Id = 1,
                CustomerId = 3,
                Total = 23.5m,
                Items = new List<Item>()
                    {
                        new Item { Id = 10, Name = "Book", Type = ItemType.Product },
                        new Item { Id = 16, Name = "Silver Membership", Type = ItemType.Membership },
                        new Item { Id = 17, Name = "Gold Membership", Type = ItemType.Membership }
                    }
            };

            //act      
            await _activateMembershipRule.ApplyAsync(order);

            //assert
            _mockCustomerAccountService.Verify(x => x.ActivateMembership(3, 10), Times.Never());
            _mockCustomerAccountService.Verify(x => x.ActivateMembership(3, 16), Times.Once());
            _mockCustomerAccountService.Verify(x => x.ActivateMembership(3, 17), Times.Once());
        }

        [Fact]
        public async Task Apply_DoesNotCallActivateMembership_WhenNoMembershipItems()
        {
            //arrange
            var order = new PurchaseOrder
            {
                Id = 1,
                CustomerId = 3,
                Total = 23.5m,
                Items = new List<Item>()
                    {
                        new Item { Id = 10, Name = "Book", Type = ItemType.Product },
                        new Item { Id = 16, Name = "Overrated Book", Type = ItemType.Product }
                    }
            };

            //act      
            await _activateMembershipRule.ApplyAsync(order);

            //assert
            _mockCustomerAccountService.Verify(x => x.ActivateMembership(It.IsAny<int>(), It.IsAny<int>()), Times.Never());
        }
    }
}