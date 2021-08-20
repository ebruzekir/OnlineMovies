using Emovies.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emovies.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<EMovieApplicationUserr> GetAll();
        EMovieApplicationUserr Get(string id);
        void Insert(EMovieApplicationUserr entity);
        void Update(EMovieApplicationUserr entity);

        void Delete(EMovieApplicationUserr entity);
        void Remove(EMovieApplicationUserr entity);
        void SaveCanges();
    }
}
