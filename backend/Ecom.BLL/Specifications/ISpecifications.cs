using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.BLL.Specifications
{
    public interface ISpecifications<T>
    {
        Expression<Func<T,bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
    }
}
