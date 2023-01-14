using Ecom.BLL.Entities;
using Ecom.BLL.Interfaces;
using Ecom.BLL.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;  
        }

        public void Add(T Entity)
             => _context.Set<T>().Add(Entity);

        public async Task<int> CountAsync(ISpecifications<T> spec)
            => await ApplySpecifcation(spec).CountAsync();

        public void Delete(T Entity)
             => _context.Set<T>().Remove(Entity);

        public async Task<T> GetByIdAsync(int id)
       => await _context.Set<T>().FindAsync(id);

        public async Task<T> GetENtityWithSpec(ISpecifications<T> spec)
            => await ApplySpecifcation(spec).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> ListAllAsync()
            => await _context.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecifications<T> spec)
            => await ApplySpecifcation(spec).ToListAsync();

        public void Update(T Entity)
        => _context.Set<T>().Update(Entity);

        private IQueryable<T> ApplySpecifcation(ISpecifications<T> specifications)
            => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specifications);
    }
}
