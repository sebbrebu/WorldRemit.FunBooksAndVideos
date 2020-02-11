using System.Threading.Tasks;
using WorldRemit.FunBooksAndVideos.Api.Models;

namespace WorldRemit.FunBooksAndVideos.Api.BusinessRules
{
    public interface IBusinessRule
    {
        Task Apply(PurchaseOrder order);
    }
}