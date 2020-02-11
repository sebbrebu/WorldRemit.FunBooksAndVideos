using System;
using System.Threading.Tasks;

namespace WorldRemit.FunBooksAndVideos.Api.Services
{
    public class CustomerAccountService : ICustomerAccountService
    {
        public async Task ActivateMembership(int customerId, int membershipId)
        {
            throw new NotImplementedException($"{nameof(CustomerAccountService)} is not implemented");
        }
    }
}