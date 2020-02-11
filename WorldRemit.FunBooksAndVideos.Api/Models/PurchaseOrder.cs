using System.Collections.Generic;

namespace WorldRemit.FunBooksAndVideos.Api.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}
