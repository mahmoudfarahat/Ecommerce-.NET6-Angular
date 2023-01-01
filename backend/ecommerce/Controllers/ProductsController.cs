using AutoMapper;
using Ecom.BLL.Entities;
using Ecom.BLL.Interfaces;
using Ecom.BLL.Specifications;
using Ecom.DAL;
using ecommerce.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IGenericRepository<Product> _productRepository;
        private IGenericRepository<ProductBrand> _brandRepository;
        private IGenericRepository<ProductType> _typeRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepository ,
            IGenericRepository<ProductBrand> brandRepository, IGenericRepository<ProductType> typeRepository, IMapper mapper

            )
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IReadOnlyList<ProductDTo>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var data = await _productRepository.ListWithSpecAsync(spec);
            var mappedData = _mapper.Map<IReadOnlyList<ProductDTo>>(data);
            return mappedData;
        }
        [HttpGet("{id}")]
        public async Task<ProductDTo> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var data = await _productRepository.GetENtityWithSpec(spec);

            var mappedData = _mapper.Map<ProductDTo>(data);
            return mappedData;
        }

        [HttpGet]
        [Route("Brands")]
        public async Task<IReadOnlyList<ProductBrand>> GetProductsBrands()
        {
            var data = await _brandRepository.ListAllAsync();

            return data;
        }
        [HttpGet]
        [Route("Types")]
        public async Task<IReadOnlyList<ProductType>> GetProductsTypes()
        {
            var data = await _typeRepository.ListAllAsync();

            return data;
        }

    }
}
