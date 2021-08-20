using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emovies.Domain.DomainModels
{
    public class MovieInShoppingCart :BaseEntity 
    {
        public Guid MovieId { get; set; }
        public Movie Movie{ get; set; }
        public Guid ShoppingCarttId { get; set; }
        public ShoppingCartt shoppingCartt { get; set; }
        public int Quantity { get; set; }

    }
}
