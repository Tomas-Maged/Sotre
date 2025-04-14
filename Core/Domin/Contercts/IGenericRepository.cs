using Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Contercts
{
   public interface IGenericRepository <TEntitey , TKey> where TEntitey : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntitey>> GetAllAsync(bool trackChages = false);
        Task<TEntitey?> GetAsync(TKey id);
        Task AddAsync(TEntitey entity);
        void Update(TEntitey entity);
        void Delete(TEntitey entity);
    }
}
