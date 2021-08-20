using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Emovies.web.Data;


using System.Security.Claims;
using Emovies.Domain.DTO;
using Emovies.Domain.DomainModels;
using Emovies.Services.Interface;

namespace Emovies.web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IProductService _productService;

        public MoviesController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Movies
        public  IActionResult Index()
        {
            var allProducts = this._productService.GetAllProducts();
            return View(allProducts);
        }
        public IActionResult AddProductToCart(Guid? id)
        {
            //var product = await _context.Movies.Where(z => z.Id.Equals(id)).FirstOrDefaultAsync();
            //AddToShoppingCartDTO model = new AddToShoppingCartDTO
            //{
            //    SelectedProduct = product,
            //    ProductId = product.Id,
            //    Quantity = 1
            //};
           var model= this._productService.GetShoppingCartInfo(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult AddProductToCard([Bind("ProductId", "Quantity")] AddToShoppingCartDTO item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._productService.AddToShoppingCart(item, userId);

            //if (item.ProductId != null && userShoppingCard != null)
            //{
            //    var product = await _context.Movies.Where(z => z.Id.Equals(item.ProductId)).FirstOrDefaultAsync();

            //    if (product != null)
            //    {
            //       MovieInShoppingCart itemToAdd = new MovieInShoppingCart
            //       {
            //            Movie = product,
            //            MovieId = product.Id,
            //            shoppingCartt = userShoppingCard,
            //            ShoppingCarttId = userShoppingCard.Id,
            //            Quantity = item.Quantity
            //        };

            //        _context.Add(itemToAdd);
            //        await _context.SaveChangesAsync();
            //    }
            //    return RedirectToAction("Index", "Movies");
            //}
            if(result)
            {
                return RedirectToAction("Index", "Movies");
            }
            return View(item);
        }

        // GET: Movies/Details/5
        public  IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = this._productService.GetDetailsForProduct(id);
                
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,M_Name,M_Image,M_Description,M_Price,Rating,time")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                //movie.Id = Guid.NewGuid();
                this._productService.CreateNewProduct(movie);
                
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = this._productService.GetDetailsForProduct(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,M_Name,M_Image,M_Description,M_Price,Rating,time")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._productService.UpdateExistingProduct(movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public  IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = this._productService.GetDetailsForProduct(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            /* var movie = await _context.Movies.FindAsync(id);
             _context.Movies.Remove(movie);
             await _context.SaveChangesAsync(); */
            this._productService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(Guid id)
        {
            return this._productService.GetDetailsForProduct(id) != null;
        }
    }
}
