using Emovies.Domain.DomainModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emovies.Domain.DTO
{
    public class ShoppingCartDTO
    {
        public List<MovieInShoppingCart> Products { get; set; }
        public double TotalPrice { get; set; }
    }
}
