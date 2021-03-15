using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace ProductAvailabilityService
{
    public class ProductAvailabilityCheckService: ProductAvailabilityCheck.ProductAvailabilityCheckBase
    {
        private readonly ILogger<ProductAvailabilityCheckService> _logger;
        private static readonly Dictionary<string, Int32> productAvailabilityInfo = new Dictionary<string, Int32>() 
        {
            {"pid001", 1},
            {"pid002", 0},
            {"pid003", 5},
	        {"pid004", 1},
            {"pid005", 0},
            {"pid006", 2}	
        };
        public ProductAvailabilityCheckService(ILogger<ProductAvailabilityCheckService> logger)
        {
            _logger = logger;
        }

        public override Task<ProductAvailabilityReply> CheckProductAvailabilityRequest(ProductAvailabilityRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ProductAvailabilityReply
            {
                IsAvailable = IsProductAvailable(request.ProductId)
            });
        }

        private bool IsProductAvailable(string productId) {
            bool isAvailable = false;

            if (productAvailabilityInfo.TryGetValue(productId, out Int32 quantity))
            {
                isAvailable = (quantity > 0) ? true : false;
            }

            return isAvailable;
        }
    }
}

