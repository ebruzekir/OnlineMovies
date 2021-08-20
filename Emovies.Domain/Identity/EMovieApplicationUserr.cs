using Emovies.Domain.DomainModels;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emovies.Domain.Identity
{
    public class EMovieApplicationUserr : IdentityUser
    {
        public String FName { get; set; }
        public String LName { get; set; }
        public String Address { get; set; }
        public virtual ShoppingCartt UserCart { get; set; }
    }
}
