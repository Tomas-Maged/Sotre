using Domian.Contercts;
using Domian.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDBContext _context;
        //private readonly Dictionary<string, object> _Repositorys;
        private readonly ConcurrentDictionary<string, object> _Repositorys;
        public UnitOfWork(StoreDBContext context)
        {
            _context = context;
            //_Repositorys = new Dictionary<string, object>();
            _Repositorys = new ConcurrentDictionary<string, object>();
        }
        //public IGenericRepository<TEntity, TKey> GenericRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        //{
        //    var type = typeof(TEntity).Name;
        //    if (!_Repositorys.ContainsKey(type))
        //    {
        //        var repository = new GenericRepository<TEntity, TKey>(_context); 
        //        _Repositorys.Add(type, repository);
        //    }
        //    return (IGenericRepository<TEntity, TKey>) _Repositorys[type];
        //}
        public IGenericRepository<TEntity, TKey> GenericRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        =>(IGenericRepository<TEntity, TKey>) 
                _Repositorys.GetOrAdd(
                typeof(TEntity).Name,
                new GenericRepository<TEntity, TKey>(_context)
            );
        

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
