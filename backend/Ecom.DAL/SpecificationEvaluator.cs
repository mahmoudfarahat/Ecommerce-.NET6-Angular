using Ecom.BLL.Entities;
using Ecom.BLL.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.DAL
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecifications<TEntity> spec)
        {
            var query = inputQuery;
            if(spec.Criteria !=null)
                query = query.Where(spec.Criteria);

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
