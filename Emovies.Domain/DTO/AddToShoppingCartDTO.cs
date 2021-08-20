
using Emovies.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Emovies.Domain.DTO
{
    public class AddToShoppingCartDTO
    {
        public Movie SelectedProduct { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
