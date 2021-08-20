using Emovies.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emovies.Services.Interface
{
   public interface IShoppingCartService
    {
        ShoppingCartDTO GetShoppingCartInfo(string userId);
        bool deleteFromShoppingCart(string userId, Guid id);
        bool orderNow(string userId);
    }

}
