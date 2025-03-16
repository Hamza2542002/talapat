using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Issuing;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IRepositories;
using Talabat.Core.IServices;
using Talabat.Core.Specifications.OrderSpecs;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration,ICartRepository cartRepository,IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrUpdatePaymentIntent(int orderId,string userEmail)
        {
            StripeConfiguration.ApiKey = _configuration["StripSettings:SecretKey"];

            var orderRepo = _unitOfWork.GetRepository<Order>();

            var order = await orderRepo.GetWithSpecsAsync(new OrderSpecifications(orderId ,userEmail));

            if (order is null) return null;

            var productRepo =  _unitOfWork.GetRepository<Product>();
            foreach (var item in order?.OredrItems ?? [])
            {
                var product = await productRepo.GetAsync(item.OrderedItemProduct.ProductId);
                if (product is null) continue;
                if (product.Price != item.Price)
                    item.Price = product.Price;
            }
            var deliverMethod = await _unitOfWork.GetRepository<DeleveryMethod>().GetAsync(order?.DeliveryMethodId ?? 0);
            if (deliverMethod is null) return null;

            order.DeliveryMethodCost = deliverMethod.Cost;

            if (string.IsNullOrEmpty(order?.PaymentIntentId))
            {
                await CreatePaymentIntent(order);
            }
            else
            {
                await UpdatePaymentIntent(order);
            }

            orderRepo.Update(order);

            await _unitOfWork.CompleteAsync();
            return order;
        }

        private async Task CreatePaymentIntent(Order order)
        {
            var options = new PaymentIntentCreateOptions()
            {
                Amount = GetAmmount(order),
                Currency = "usd",
                PaymentMethodTypes = new List<string>() { "card" }
            };
            var paymentServie = new PaymentIntentService();
            var paymentIntent = await paymentServie.CreateAsync(options);
            order.PaymentIntentId = paymentIntent.Id;
            order.ClientSecret = paymentIntent.ClientSecret;
        }

        private async Task UpdatePaymentIntent(Order order)
        {
            var options = new PaymentIntentUpdateOptions()
            {
                Amount = GetAmmount(order),
            };

            var paymentServie = new PaymentIntentService();
            await paymentServie.UpdateAsync(order.PaymentIntentId,options);
        }

        private long GetAmmount(Order order)
        {
            return (long)order?.OredrItems?.Sum(item => item.Price * item.Quantity * 100) + (long)order.DeliveryMethodCost * 100;
                
        }
    }
}
