using Emovies.Domain.Identity;
using Emovies.Repository.Interface;
using Emovies.web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emovies.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<EMovieApplicationUserr> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<EMovieApplicationUserr>();
        }
        public IEnumerable<EMovieApplicationUserr> GetAll()
        {
            return entities.AsEnumerable();
        }

        public EMovieApplicationUserr Get(string id)
        {
            return entities
                .Include(z => z.UserCart)
                    .Include("UserCart.MovieInShoppingCarts")
                    .Include("UserCart.MovieInShoppingCarts.Movie")
                .SingleOrDefault(s => s.Id==id);
        }
        public void Insert(EMovieApplicationUserr entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(EMovieApplicationUserr entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(EMovieApplicationUserr entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
        public void Remove(EMovieApplicationUserr entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public EMovieApplicationUserr Get(Guid? id)
        {
            throw new NotImplementedException();
        }

        public void SaveCanges()
        {
            throw new NotImplementedException();
        }
    }
}
