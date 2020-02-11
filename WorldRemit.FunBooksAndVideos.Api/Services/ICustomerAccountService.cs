using System.Threading.Tasks;

namespace WorldRemit.FunBooksAndVideos.Api.Services
{
    public interface ICustomerAccountService
    {
        Task ActivateMembership(int customerId, int membershipId);
    }
}