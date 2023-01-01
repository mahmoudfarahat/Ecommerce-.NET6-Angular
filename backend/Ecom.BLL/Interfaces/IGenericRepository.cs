using Ecom.BLL.Entities;
using Ecom.BLL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();

        Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecifications<T> spec);
        Task<T> GetENtityWithSpec(ISpecifications<T> spec);
        void Add(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
    }
}
