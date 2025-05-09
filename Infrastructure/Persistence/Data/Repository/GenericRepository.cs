﻿using Domian.Contercts;
using Domian.Models;
using Microsoft.EntityFrameworkCore;
using Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Repository
{
    class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public StoreDBContext _Context;

        public GenericRepository(StoreDBContext Context) 
        {
            _Context = Context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChages = false)
        {
            if (typeof(TEntity) == typeof(Proudect))
            {
                return trackChages ?
                await _Context.Proudects.Take(5).Include(p=>p.ProudectBrand).Include(p=>p.ProudectType).ToListAsync() as IEnumerable<TEntity>
                : await _Context.Proudects.Include(p => p.ProudectBrand).Include(p => p.ProudectType).ToListAsync() as IEnumerable<TEntity>;

            }
            return  trackChages ? 
                await _Context.Set<TEntity>().ToListAsync()
                : await _Context.Set<TEntity>().AsNoTracking().ToListAsync();

            //if (trackChages)
            //{
            //    return await _Context.Set<TEntity>().ToListAsync();
            //}
            //return await _Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Proudect))
            {
                //return await _Context.Proudects.Include(p => p.ProudectBrand).Include(p => p.ProudectType).FirstOrDefaultAsync(p => p.Id.Equals(id)) as TEntity;
                return await _Context.Proudects.Where(p => p.Id.Equals(id)).Include(p => p.ProudectBrand).Include(p => p.ProudectType).FirstOrDefaultAsync() as TEntity;

            }
            return await _Context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _Context.AddAsync(entity);

        }
        public void Update(TEntity entity)
        {
            _Context.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _Context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> Spec, bool trackChages = false)
        {
            return await ApplySpecifications(Spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(ISpecifications<TEntity, TKey> Spec)
        {
            return await ApplySpecifications(Spec).FirstOrDefaultAsync();
        }
        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> Spec)
        {
            return await ApplySpecifications(Spec).CountAsync();
        }
        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> spec)
        {
            return SpecificationEva1uator.GetQuery(_Context.Set<TEntity>(), spec);
        }

    }

}

