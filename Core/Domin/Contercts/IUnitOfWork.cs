using Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Contercts
{
   public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        
        IGenericRepository<TEntity,TKey> GenericRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    }
}
