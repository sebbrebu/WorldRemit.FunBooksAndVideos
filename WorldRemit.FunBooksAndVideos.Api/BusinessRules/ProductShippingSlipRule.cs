using System.Linq;
using System.Threading.Tasks;
using WorldRemit.FunBooksAndVideos.Api.Models;
using WorldRemit.FunBooksAndVideos.Api.Services;

namespace WorldRemit.FunBooksAndVideos.Api.BusinessRules
{
    public class ProductShippingSlipRule : IBusinessRule
    {
        private readonly IShippingService _shippingService;

        public ProductShippingSlipRule(IShippingService shippingService)
        {
            _shippingService = shippingService;
        }

        public async Task Apply(PurchaseOrder order)
        {
            var products = order.Items.Where(i => i.Type == ItemType.Product);

            if (products.Any())
            {
                await _shippingService.GenerateShippingSlip(order.CustomerId, products);
            }
        }
    }
}