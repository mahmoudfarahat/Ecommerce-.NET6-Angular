using Ecom.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.BLL.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
            : base(x =>
            (!productParams.BrandId.HasValue || x.ProductBrandId ==   productParams.BrandId) &&
               (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

        }

        public ProductsWithTypesAndBrandsSpecification(int id)
            : base(x => x.Id == id
            
            )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

        }
    }
}
