
using Emovies.Domain.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emovies.Domain.DomainModels
{
    public class Order : BaseEntity
    {
       
        public string UserId { get; set; }
        public EMovieApplicationUserr User { get; set; }
        public IEnumerable<ProductInOrder> Products { get; set; }

    }
}
