using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Issuing;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IRepositories;
using Talabat.Core.IServices;
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
        public async Task<CustomerCart?> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripSettings:SecretKey"];

            var cart = await _cartRepository.GetCustomerCartAsync(basketId);
            
            if (cart is null) return null;

            var productRepo =  _unitOfWork.GetRepository<Product>();
            foreach (var item in cart?.CartItems ?? [])
            {
                var product = await productRepo.GetAsync(item.ItemId);
                if (product is null) continue;
                if (product.Price != item.Price)
                    item.Price = product.Price;
            }
            var deliverMethod = await _unitOfWork.GetRepository<DeleveryMethod>().GetAsync(cart.DeliveryMethodId);
            if (deliverMethod is null) return null;

            cart.DeliveryMethodCost = deliverMethod.Cost;

            if (string.IsNullOrEmpty(cart?.PaymentIntentId))
            {
                await CreatePaymentIntent(cart);
            }
            else
            {
                await UpdatePaymentIntent(cart);
            }
            

            return await _cartRepository.UpdateCustomerCartAsync(cart);
        }

        private async Task CreatePaymentIntent(CustomerCart cart)
        {
            var options = new PaymentIntentCreateOptions()
            {
                Amount = GetAmmount(cart),
                Currency = "usd",
                PaymentMethodTypes = new List<string>() { "card" }
            };
            var paymentServie = new PaymentIntentService();
            var paymentIntent = await paymentServie.CreateAsync(options);
            cart.PaymentIntentId = paymentIntent.Id;
            cart.ClientSecret = paymentIntent.ClientSecret;
        }

        private async Task UpdatePaymentIntent(CustomerCart cart)
        {
            var options = new PaymentIntentUpdateOptions()
            {
                Amount = GetAmmount(cart),
            };

            var paymentServie = new PaymentIntentService();
            await paymentServie.UpdateAsync(cart.PaymentIntentId,options);
        }

        private long GetAmmount(CustomerCart cart)
        {
            return (long)cart?.CartItems?.Sum(item => item.Price * item.Quantity * 100) + (long)cart.DeliveryMethodCost * 100;
                
        }
    }
}
