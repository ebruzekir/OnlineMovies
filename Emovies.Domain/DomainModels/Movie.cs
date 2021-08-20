
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emovies.Domain.DomainModels
{
    public class Movie : BaseEntity
    {
        
        [Required]
        public String M_Name { get; set; }
        [Required]
        public String M_Image { get; set; }
        [Required]
        public String M_Description { get; set; }
        [Required]
        public int M_Price { get; set; }
        [Required]
        public int Rating { get; set; }
        public String time { get; set; }
        public virtual ICollection<MovieInShoppingCart> MovieInShoppingCarts { get; set; }
        public IEnumerable<ProductInOrder> Orders { get; set; }

    }
}
