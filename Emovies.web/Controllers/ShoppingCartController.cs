
using Emovies.Domain.DomainModels;
using Emovies.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EMovies.Web.Controllers
{
    public class ShoppingCartController : Controller
    {


        private readonly IShoppingCartService _shoppingcartservice;

        public ShoppingCartController(IShoppingCartService shoppingcartservice)
        {
            _shoppingcartservice = shoppingcartservice;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);



            //var loggedInUser = await _context.Users.Where(z => z.Id == userId)
            //    .Include("UserCart")
            //    .Include("UserCart.MovieInShoppingCarts")
            //    .Include("UserCart.MovieInShoppingCarts.Movie")
            //    .FirstOrDefaultAsync();

            //var userShoppingCart = loggedInUser.UserCart;

            //var AllProducts = userShoppingCart.MovieInShoppingCarts.ToList();

            //var allProductPrice = AllProducts.Select(z => new
            //{
            //    ProductPrice = z.Movie.M_Price,
            //    Quanitity = z.Quantity
            //}).ToList();

            //var totalPrice = 0;


            //foreach (var item in allProductPrice)
            //{
            //    totalPrice += item.Quanitity * item.ProductPrice;
            //}


            //ShoppingCartDTO scDto = new ShoppingCartDTO
            //{
            //    Products = AllProducts,
            //    TotalPrice = totalPrice
            //};


            return View(this._shoppingcartservice.GetShoppingCartInfo(userId));
        }

        public IActionResult DeleteFromShoppingCart(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //if (!string.IsNullOrEmpty(userId) && id != null)
            //{
            //    //Select * from Users Where Id LIKE userId

            //    var loggedInUser = await _context.Users.Where(z => z.Id == userId)
            //        .Include("UserCart")
            //        .Include("UserCart.MovieInShoppingCarts")
            //        .Include("UserCart.MovieInShoppingCarts.Movie")
            //        .FirstOrDefaultAsync();

            //    var userShoppingCart = loggedInUser.UserCart;

            //    var itemToDelete = userShoppingCart.MovieInShoppingCarts.Where(z => z.Movie.Id.Equals(id)).FirstOrDefault();

            //    userShoppingCart.MovieInShoppingCarts.Remove(itemToDelete);

            //    _context.Update(userShoppingCart);
            //    await _context.SaveChangesAsync();
            //}
           var result= this._shoppingcartservice.deleteFromShoppingCart(userId, id);
           if(result)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
        }

        public IActionResult OrderNow()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //if (!string.IsNullOrEmpty(userId))
            //{
            //    //Select * from Users Where Id LIKE userId

            //    var loggedInUser = await _context.Users.Where(z => z.Id == userId)
            //        .Include("UserCart")
            //        .Include("UserCart.MovieInShoppingCarts")
            //        .Include("UserCart.MovieInShoppingCarts.Movie")
            //        .FirstOrDefaultAsync();

            //    var userShoppingCart = loggedInUser.UserCart;

            //    Order order = new Order
            //    {
            //        Id = Guid.NewGuid(),
            //        User = loggedInUser,
            //        UserId = userId
            //    };

            //    _context.Add(order);
            //    await _context.SaveChangesAsync();

            //    List<ProductInOrder> productInOrders = new List<ProductInOrder>();

            //    var result = userShoppingCart.MovieInShoppingCarts.Select(z => new ProductInOrder
            //    {
            //        ProductId = z.Movie.Id,
            //        SelectedProduct = z.Movie,
            //        OrderId = order.Id,
            //        UserOrder = order
            //    }).ToList();

            //    productInOrders.AddRange(result);

            //    foreach (var item in productInOrders)
            //    {
            //        _context.Add(item);
            //    }
            //    await _context.SaveChangesAsync();

            //    loggedInUser.UserCart.MovieInShoppingCarts.Clear();

            //    _context.Update(loggedInUser);
            //    await _context.SaveChangesAsync();
            //}

            var result = this._shoppingcartservice.orderNow(userId);
            if(result)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
        }
    }
}
