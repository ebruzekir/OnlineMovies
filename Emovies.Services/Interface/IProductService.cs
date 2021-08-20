using Emovies.Domain.DomainModels;
using Emovies.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emovies.Services.Interface
{
   public interface IProductService
    {
        List<Movie> GetAllProducts();
        Movie GetDetailsForProduct(Guid? id);
        void CreateNewProduct(Movie p);
        void UpdateExistingProduct(Movie p);
        AddToShoppingCartDTO GetShoppingCartInfo(Guid? id);
        void DeleteProduct(Guid? id);
        bool AddToShoppingCart(AddToShoppingCartDTO item, string userID);

    }
}
