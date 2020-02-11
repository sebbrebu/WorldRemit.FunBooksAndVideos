using System.Collections.Generic;
using System.Threading.Tasks;
using WorldRemit.FunBooksAndVideos.Api.Models;

namespace WorldRemit.FunBooksAndVideos.Api.Services
{
    public interface IShippingService
    {
        Task GenerateShippingSlip(int customerId, IEnumerable<Item> items);
    }
}