using Ecom.BLL.Entities;
using Ecom.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            var data = await _productRepository.GetProductsAsync();
            return data;
        }
        [HttpGet("{id}")]
        public async Task<Product> GetProduct(int id)
        {
            var data = await _productRepository.GetProductByIdAsync(id);
            return data;
        }

        [HttpGet]
        [Route("Brands")]
        public async Task<IReadOnlyList<ProductBrand>> GetProductsBrands()
        {
            var data = await _productRepository.GetProductBrandsAsync();

            return data;
        }
        [HttpGet]
        [Route("Types")]
        public async Task<IReadOnlyList<ProductType>> GetProductsTypes()
        {
            var data = await _productRepository.GetProductTypesAsync();

            return data;
        }

    }
}
