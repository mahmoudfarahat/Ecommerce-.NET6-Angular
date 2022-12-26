using Ecom.BLL.Entities;
using Ecom.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
            => await _context.ProductBrands.ToListAsync();

        public async Task<Product> GetProductByIdAsync(int id)
            => await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(x => x.Id == id);
        public async Task<IReadOnlyList<Product>> GetProductsAsync()
            => await _context.Products.Include(p => p.ProductType).Include(p => p.ProductBrand).ToListAsync();

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
            =>await _context.ProductTypes.ToListAsync();
    }
}
