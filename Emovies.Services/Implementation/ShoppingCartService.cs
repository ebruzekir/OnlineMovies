using Emovies.Domain.DomainModels;
using Emovies.Domain.DTO;
using Emovies.Repository.Interface;
using Emovies.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emovies.Services.Implementation
{
    public class ShoppingCartService : IShoppingCartService

    {
        private readonly IRepository<ShoppingCartt> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ProductInOrder> _productInOrderRepository;
        private readonly IUserRepository _userRepository;
       public ShoppingCartService(IRepository<ShoppingCartt> shoppingCartRepository, IRepository<Order> orderRepository, IRepository<ProductInOrder> productInOrderRepository, IUserRepository userRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _productInOrderRepository = productInOrderRepository;
            _orderRepository = orderRepository;
        }

        public bool deleteFromShoppingCart(string userId, Guid id)
        {

            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.MovieInShoppingCarts.Where(z => z.Movie.Id.Equals(id)).FirstOrDefault();

                userShoppingCart.MovieInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);
                return true;
            }
            return false;
        }

        public ShoppingCartDTO GetShoppingCartInfo(string userId)
        {

            var loggedInUser = this._userRepository.Get(userId);

            var userShoppingCart = loggedInUser.UserCart;

            var AllProducts = userShoppingCart.MovieInShoppingCarts.ToList();

            var allProductPrice = AllProducts.Select(z => new
            {
                ProductPrice = z.Movie.M_Price,
                Quanitity = z.Quantity
            }).ToList();

            var totalPrice = 0;


            foreach (var item in allProductPrice)
            {
                totalPrice += item.Quanitity * item.ProductPrice;
            }


            ShoppingCartDTO scDto = new ShoppingCartDTO
            {
                Products = AllProducts,
                TotalPrice = totalPrice
            };
            return scDto;
        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<ProductInOrder> productInOrders = new List<ProductInOrder>();

                var result = userShoppingCart.MovieInShoppingCarts.Select(z => new ProductInOrder
                {
                    ProductId = z.Movie.Id,
                    SelectedProduct = z.Movie,
                    OrderId = order.Id,
                    UserOrder = order
                }).ToList();

                productInOrders.AddRange(result);

                foreach (var item in productInOrders)
                {
                    this._productInOrderRepository.Insert(item);
                }
               

                loggedInUser.UserCart.MovieInShoppingCarts.Clear();
                this._userRepository.Update(loggedInUser);
                return true;

            }
            return false;

        }
    }
}
