using Emovies.Domain.Identity;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emovies.Domain.DomainModels
{
    public class ShoppingCartt : BaseEntity
    {
       
        public String OwnerId { get; set; }
        public EMovieApplicationUserr Owner { get; set; }
    
        public virtual ICollection<MovieInShoppingCart> MovieInShoppingCarts { get; set; }

    }
}

