using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldRemit.FunBooksAndVideos.Api.Models;

namespace WorldRemit.FunBooksAndVideos.Api.Services
{
    public class ShippingService : IShippingService
    {
        public async Task GenerateShippingSlip(int customerId, IEnumerable<Item> items)
        {
            throw new NotImplementedException($"{nameof(ShippingService)} is not implemented");
        }
    }
}