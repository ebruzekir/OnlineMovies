
using Emovies.Domain.DomainModels;
using Emovies.Domain.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emovies.web.Data
{
    public class ApplicationDbContext : IdentityDbContext<EMovieApplicationUserr>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<ShoppingCartt> ShoppingCarts { get; set; }
        public virtual DbSet<MovieInShoppingCart> MovieInShoppingCarts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Movie>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd(); // ke se kreira nov id na sekoe kreiranje na nov produkt
            builder.Entity<ShoppingCartt>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<MovieInShoppingCart>()
                 .HasKey(z => new { z.MovieId, z.ShoppingCarttId });
            builder.Entity<MovieInShoppingCart>()
                .HasOne(z => z.Movie)
                .WithMany(z => z.MovieInShoppingCarts)
                .HasForeignKey(z => z.ShoppingCarttId);
            builder.Entity<MovieInShoppingCart>()
            .HasOne(z => z.shoppingCartt)
            .WithMany(z => z.MovieInShoppingCarts)
            .HasForeignKey(z => z.MovieId);
            builder.Entity<ShoppingCartt>()
                .HasOne<EMovieApplicationUserr>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<ShoppingCartt>(z => z.OwnerId);
            builder.Entity<ProductInOrder>()
                .HasKey(z => new { z.ProductId, z.OrderId });
            builder.Entity<ProductInOrder>()
                .HasOne(z => z.SelectedProduct)
                .WithMany(z => z.Orders)
                .HasForeignKey(z => z.ProductId);
            builder.Entity<ProductInOrder>()
            .HasOne(z => z.UserOrder)
            .WithMany(z => z.Products)
            .HasForeignKey(z => z.OrderId);
        }
    }
}
