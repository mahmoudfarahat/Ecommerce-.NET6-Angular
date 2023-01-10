using AutoMapper;
using Ecom.BLL.Entities;
using Ecom.BLL.Interfaces;
using Ecom.BLL.Specifications;
using Ecom.DAL;
using ecommerce.Dto;
using ecommerce.Errors;
using ecommerce.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    [Authorize]
    public class ProductsController : BaseApiController
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
        public async Task<ActionResult<Pagination<ProductDTo>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductsWithFilterForCountSpecifications(productParams);

            var totalItems = await _productRepository.CountAsync(countSpec);

            var data = await _productRepository.ListWithSpecAsync(spec);
            var mappedData = _mapper.Map<IReadOnlyList<ProductDTo>>(data);

            var paginatedList = new Pagination<ProductDTo>(productParams.PageIndex, productParams.PageSize, totalItems, mappedData);
          
            return Ok(paginatedList);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTo>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var data = await _productRepository.GetENtityWithSpec(spec);
            if (data is null)
                return NotFound(new ApiResponse(404));

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
