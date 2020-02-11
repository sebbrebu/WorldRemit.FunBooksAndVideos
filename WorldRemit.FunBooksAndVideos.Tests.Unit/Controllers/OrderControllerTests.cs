using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WorldRemit.FunBooksAndVideos.Api.Controllers;
using WorldRemit.FunBooksAndVideos.Api.Models;
using WorldRemit.FunBooksAndVideos.Api.Services;
using Xunit;

namespace WorldRemit.FunBooksAndVideos.Tests.Unit.Controllers
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly OrderController _orderController;

        public OrderControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _orderController = new OrderController(_mockOrderService.Object);
        }

        private PurchaseOrder GetTestPurchaseOrder()
        {
            return new PurchaseOrder
            {
                Id = 1,
                CustomerId = 3,
                Total = 23.5m,
                Items = new List<Item>()
                {
                    new Item { Id = 10, Name = "Book", Type = ItemType.Product }
                }
            };
        }

        [Fact]
        public async Task Process_ShouldReturnOk_WhenSuccessful()
        {
            //arrange
            var order = GetTestPurchaseOrder();

            //act
            var result = await _orderController.Process(order);

            //assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Process_CallsProcessOrderMethod()
        {
            //arrange
            var order = GetTestPurchaseOrder();

            //act
            await _orderController.Process(order);

            //assert
            _mockOrderService.Verify(x => x.ProcessAsync(order), Times.Once);
        }
    }
}
