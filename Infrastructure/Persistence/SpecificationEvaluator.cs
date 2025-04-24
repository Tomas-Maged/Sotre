using Domian.Contercts;
using Domian.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    static class SpecificationEva1uator
    {
        public static IQueryable<TEntity> GetQuery<TEntity, TKey>
            (
            IQueryable<TEntity> InputQuery,
            ISpecifications<TEntity,TKey> Spec
            ) 
            where TEntity : BaseEntity<TKey>
        {
            var qury = InputQuery;
            if (Spec.criteria != null)
            
                qury = qury.Where(Spec.criteria);
            if (Spec.OrderBy != null)
            
                qury = qury.OrderBy(Spec.OrderBy);
            else if (Spec.OrderByDescending != null)

                qury = qury.OrderByDescending(Spec.OrderByDescending);
            if (Spec.IsPagintion)
                qury = qury.Skip(Spec.Skip).Take(Spec.Take);

            qury =   Spec.IncludeExpression.Aggregate(qury, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));



            return qury;
        } 
        
        }

    }

