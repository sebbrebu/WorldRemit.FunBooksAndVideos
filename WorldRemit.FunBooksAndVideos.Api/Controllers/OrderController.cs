using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorldRemit.FunBooksAndVideos.Api.Models;
using WorldRemit.FunBooksAndVideos.Api.Services;

namespace WorldRemit.FunBooksAndVideos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Process(PurchaseOrder order)
        {
            await _orderService.ProcessOrder(order);

            return Ok();
        }
    }
}