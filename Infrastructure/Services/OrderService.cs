using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<DeliveryMethod> _dmRepo;
        private readonly IBasketRepository _basketRepo;
        private readonly StoreContext context;

        public OrderService(IGenericRepository<Order> orderRepo, 
            IGenericRepository<Product> productRepo, 
            IGenericRepository<DeliveryMethod> dmRepo, 
            IBasketRepository basketRepo,
            StoreContext context)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _dmRepo = dmRepo;
            _basketRepo = basketRepo;
            this.context = context;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, int basketId, Address shippingToAddress)
        {
            //var basket = await _basketRepo.GetBasketAsync(basketId);
            var basketItems = context.BasketItems.Where(i => i.BasketId == basketId).ToList();

            var orderItems = new List<OrderItem>();
            
            foreach(var basketItem in basketItems)
            {
                var productItemFromDB = await _productRepo.GetByIdAsync(basketItem.Id); // To be checked (I think it should be basketItem.productId)
                var itemOrdered = new ProductItemOrdered(productItemFromDB.Id, productItemFromDB.Name);
                var orderItem = new OrderItem(itemOrdered, productItemFromDB.Price, basketItem.Quantity);
                orderItems.Add(orderItem);
            }

            var deliveryMethod = await _dmRepo.GetByIdAsync(deliveryMethodId);

            var subTotal = orderItems.Sum(oi => oi.Price * oi.Qunatity);

            var order = new Order(orderItems, buyerEmail, shippingToAddress, deliveryMethod, subTotal);

            await _orderRepo.AddAsync(order);

            // Do we need to delete the basket here or not?

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            return await _dmRepo.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);
            return await _orderRepo.GetByIdAsync(id, spec);

        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
            return await _orderRepo.GetAllAsync(spec);
        }

        public async Task CancelOrderAsync(int id)
        {
            await _orderRepo.DeleteAsync(id);
        }

        public async Task UpdateOrder(int id, Order order)
        {
            await _orderRepo.UpdateAsync(id, order);
        }
    }
}
