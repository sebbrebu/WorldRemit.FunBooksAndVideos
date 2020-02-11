using System.Threading.Tasks;
using WorldRemit.FunBooksAndVideos.Api.Models;

namespace WorldRemit.FunBooksAndVideos.Api.Services
{
    public interface IOrderService
    {
        Task ProcessOrder(PurchaseOrder order);
    }
}
