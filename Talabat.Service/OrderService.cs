using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IRepositories;
using Talabat.Core.IServices;
using Talabat.Repository.Data;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IUnitOfWork _unitOfWork;
        private IGenericRepository<Product> _productRepo;
        private IGenericRepository<DeleveryMethod> _deleveryRepo;
        private IGenericRepository<Order> _orderRepo;

        public OrderService(ICartRepository cartRepo,
            IUnitOfWork unitOfWork)
        {
            _cartRepo = cartRepo;
            _unitOfWork = unitOfWork;
            _productRepo = _unitOfWork.GetRepository<Product>();
            _deleveryRepo = _unitOfWork.GetRepository<DeleveryMethod>();
            _orderRepo = _unitOfWork.GetRepository<Order>();
        }
        public async Task<Order?> CreateOrderAsync(
            string customerEmail, 
            string cartId, 
            int deleveryMethodId, 
            Address orderAddress)
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
                    var product = await _productRepo.GetAsync(item.ItemId);

                    if (product is null) return null;
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

            var deleveryMethod = await _deleveryRepo.GetAsync(deleveryMethodId);

            if (deleveryMethod is null) return null;

            // 5- Create Order

            var order = new Order(customerEmail, orderItems, orderAddress, deleveryMethod, subTotal);

            // 6- Save To Data Base

            await _orderRepo.AddAsync(order);

            // Saving TO DB

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return null;

            return order;
        }

        public Task<Order> GetOrderForUserAsync(int OrderId, string userEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string userEmail)
        {
            throw new NotImplementedException();
        }
    }
}
