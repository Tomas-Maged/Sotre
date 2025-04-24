﻿using Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Contercts
{
    public interface ISpecifications<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
         Expression<Func<TEntity, bool>>?  criteria { get; set; }
       List<Expression<Func<TEntity, object>>> IncludeExpression { get; set; }
        public Expression< Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; set; }
         int Skip { get; set; }
         int Take { get; set; }
         bool IsPagintion { get; set; } 
    }
}
