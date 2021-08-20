using Emovies.Domain.DomainModels;
using Emovies.Domain.DTO;
using Emovies.Repository.Interface;
using Emovies.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Emovies.Services.Implementation
{
    public class ProductService : IProductService

    {
        private readonly IRepository<Movie> _productRepository;
        private readonly IRepository<MovieInShoppingCart> _movieInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        
        public ProductService(IRepository<Movie> productRepository, IUserRepository userRepository, IRepository<MovieInShoppingCart> movieInShoppingCartRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _movieInShoppingCartRepository = movieInShoppingCartRepository;
        }
        
        public bool AddToShoppingCart(AddToShoppingCartDTO item, string userID)
        {
            var user = this._userRepository.Get(userID);
            var userShoppingCard = user.UserCart;
            if (item.ProductId != null && userShoppingCard != null)
            {
                var product = this.GetDetailsForProduct(item.ProductId);

                if (product != null)
                {
                    MovieInShoppingCart itemToAdd = new MovieInShoppingCart
                    {
                        Movie = product,
                        MovieId = product.Id,
                        shoppingCartt = userShoppingCard,
                        ShoppingCarttId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    this._movieInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
                
            }
            return false;
        }

        public void CreateNewProduct(Movie p)
        {
            this._productRepository.Insert(p);
        }

        public void DeleteProduct(Guid? id)
        {
            var movie = this.GetDetailsForProduct(id);
            this._productRepository.Delete(movie);
        }

        public List<Movie> GetAllProducts()
        {
            return this._productRepository.GetAll().ToList();
        }

        public Movie GetDetailsForProduct(Guid? id)
        {
            return this._productRepository.Get(id);
        }

        public AddToShoppingCartDTO GetShoppingCartInfo(Guid? id)
        {
            var product = this.GetDetailsForProduct(id);
            AddToShoppingCartDTO model = new AddToShoppingCartDTO
            {
                SelectedProduct = product,
                ProductId = product.Id,
                Quantity = 1
            };
            return model;
            
            }

        public void UpdateExistingProduct(Movie p)
        {
            this._productRepository.Update(p);
        }
    }
}
