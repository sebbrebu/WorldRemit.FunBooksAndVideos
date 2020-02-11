using System.Collections.Generic;
using System.Threading.Tasks;
using WorldRemit.FunBooksAndVideos.Api.BusinessRules;
using WorldRemit.FunBooksAndVideos.Api.Models;

namespace WorldRemit.FunBooksAndVideos.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IEnumerable<IBusinessRule> _businessRules;

        public OrderService(IEnumerable<IBusinessRule> businessRules)
        {
            _businessRules = businessRules;
        }

        public async Task ProcessAsync(PurchaseOrder order)
        {
            foreach (var rule in _businessRules)
            {
                await rule.ApplyAsync(order);
            }
        }
    }
}