using Microsoft.AspNetCore.Identity;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IRepositories;
using Talabat.Core.IServices;
using Talabat.Core.Specifications.OrderSpecs;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPaymentService _paymentService;

        public OrderService(ICartRepository cartRepo,
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IPaymentService paymentService)
        {
            _cartRepo = cartRepo;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _paymentService = paymentService;
        }
        public async Task<Order?> CreateOrderAsync(
            string customerEmail, 
            string cartId, 
            //int deleveryMethodId,
            Core.Entities.Order_Aggregate.Address orderAddress)
        {
            // 1- Get Cart From Cart Repo

            var cart = await _cartRepo.GetCustomerCartAsync(cartId);
            if (cart is null) return null;

            // 2- Get Selected Items From Product Repo

            var cartProducts = cart?.CartItems?.ToList();
            var orderItems = new List<OrderItem>();
            if(cartProducts?.Count > 0)
            {
                foreach (var item in cartProducts)
                {
                    var product = await _unitOfWork.GetRepository<Product>().GetAsync(item.ItemId);

                    if (product is null) continue;
                    orderItems.Add(
                        new OrderItem(
                            new OrderedItemProduct(product.Id, product.Name, product.PicUrl),
                            product.Price, 
                            item.Quantity
                            )
                        );
                }
            }

            if (orderItems.Count <= 0)
                return null;

            // 3- Calculate SubTotal

            var subTotal = orderItems.Sum(orderItem => orderItem.Quantity * orderItem.Price);

            // 4- Get Delevery Method From Delevery Method Repo

            var deleveryMethod = await _unitOfWork.GetRepository<DeleveryMethod>().GetAsync(cart.DeliveryMethodId);

            if (deleveryMethod is null) return null;

            var orderRepo = _unitOfWork.GetRepository<Order>();

            var existingOrders = await orderRepo
                .GetAllWithSpecsAsync(new OrderSpecifications(cart.PaymentIntentId));
            foreach (var item in existingOrders)
            {
                orderRepo.Delete(item);
                await _paymentService.CreateOrUpdatePaymentIntent(cartId);
            }
            // 5- Create Order

            var order = new Order(
                customerEmail,
                orderItems,
                orderAddress,
                deleveryMethod,
                subTotal,
                cart.PaymentIntentId);

            // 6- Save To Data Base

            await _unitOfWork.GetRepository<Order>().AddAsync(order);

            // Saving TO DB

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return null;

            return order;
        }

        public async Task<IReadOnlyList<DeleveryMethod>?> GetDeleveryMethods()
        {
            return await _unitOfWork.GetRepository<DeleveryMethod>().GetAllAsync();
        }

        public async Task<Order?> GetOrderForUserAsync(int orderId, string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null) return null;
            var specs = new OrderSpecifications(orderId,userEmail);
            var order = await _unitOfWork.GetRepository<Order>().GetWithSpecsAsync(specs);
            return order;
        }

        public async Task<IReadOnlyList<Order>?> GetOrdersForUserAsync(string userEmail,OrderSpecsParams specsParams)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null) return null;

            var specs = new OrderSpecifications(userEmail,specsParams);
            var orders = await _unitOfWork.GetRepository<Order>().GetAllWithSpecsAsync(specs);
            return orders;
        }
    

    }
}
