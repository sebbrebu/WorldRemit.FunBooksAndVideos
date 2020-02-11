using System.Linq;
using System.Threading.Tasks;
using WorldRemit.FunBooksAndVideos.Api.Models;
using WorldRemit.FunBooksAndVideos.Api.Services;

namespace WorldRemit.FunBooksAndVideos.Api.BusinessRules
{
    public class ActivateMembershipRule : IBusinessRule
    {
        private readonly ICustomerAccountService _customerAccountService;

        public ActivateMembershipRule(ICustomerAccountService customerAccountService)
        {
            _customerAccountService = customerAccountService;
        }

        public async Task ApplyAsync(PurchaseOrder order)
        {
            var mermberships = order.Items.Where(i => i.Type == ItemType.Membership);

            foreach (var mermbership in mermberships)
            {
                await _customerAccountService.ActivateMembership(order.CustomerId, mermbership.Id);
            }
        }
    }
}