using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
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


        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<Product> productRepo, IGenericRepository<DeliveryMethod> dmRepo, IBasketRepository basketRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _dmRepo = dmRepo;
            _basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrder(string buyerEmail, int deliveryMethodId, string basketId, Address shippingToAddress)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);

            var orderItems = new List<OrderItem>();
            
            foreach(var basketItem in basket.BasketItems)
            {
                var productItemFromDB = await _productRepo.GetByIdAsync(basketItem.Id); // To be checked (I think it must be basketItem.productId)
                var itemOrdered = new ProductItemOrdered(productItemFromDB.Id, productItemFromDB.Name, productItemFromDB.Pictures);
                var orderItem = new OrderItem(itemOrdered, productItemFromDB.Price, basketItem.Quantity);
                orderItems.Add(orderItem);
            }

            var deliveryMethod = await _dmRepo.GetByIdAsync(deliveryMethodId);

            var subTotal = orderItems.Sum(oi => oi.Price * oi.Qunatity);

            var order = new Order(orderItems, buyerEmail, shippingToAddress, deliveryMethod, subTotal);

            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
