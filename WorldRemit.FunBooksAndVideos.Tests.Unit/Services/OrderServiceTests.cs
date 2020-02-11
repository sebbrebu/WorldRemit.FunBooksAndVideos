using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using WorldRemit.FunBooksAndVideos.Api.BusinessRules;
using WorldRemit.FunBooksAndVideos.Api.Models;
using WorldRemit.FunBooksAndVideos.Api.Services;
using Xunit;

namespace WorldRemit.FunBooksAndVideos.Tests.Unit.Services
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task Apply_ShouldBeCalled_ForEachBusinessRulePassedIntoService()
        {
            //arrange
            var rule1 = new Mock<IBusinessRule>();
            var rule2 = new Mock<IBusinessRule>();
            var rule3 = new Mock<IBusinessRule>();

            var service = new OrderService(new List<IBusinessRule>() { rule1.Object, rule2.Object });

            var order = new PurchaseOrder
            {
                Id = 1,
                CustomerId = 3,
                Total = 23.5m,
                Items = new List<Item>()
                {
                    new Item { Id = 10, Name = "Book", Type = ItemType.Product }
                }
            };

            //act      
            await service.ProcessAsync(order);

            //assert
            rule1.Verify(x => x.ApplyAsync(order), Times.Once);
            rule2.Verify(x => x.ApplyAsync(order), Times.Once);
            rule3.Verify(x => x.ApplyAsync(order), Times.Never);
        }
    }
}
