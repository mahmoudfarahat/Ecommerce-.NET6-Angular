using Ecom.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.BLL.Specifications
{
    public class ProductsWithFilterForCountSpecifications : BaseSpecification<Product>
    {
        public ProductsWithFilterForCountSpecifications(ProductSpecParams productParams) : base(x =>
           (string.IsNullOrEmpty(productParams.Seacrh) || x.Name.ToLower().Contains(productParams.Seacrh)) &&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
               (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {

        }
    }
}
