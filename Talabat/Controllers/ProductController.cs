using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications.ProductSpecs;
using Talabat.Dtos;
using Talabat.Helpers;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(
        IGenericRepository<Product> productsRepository,
        IGenericRepository<Brand> brandsRepository,
        IGenericRepository<Category> categoriesRepository,
        IMapper mapper) : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepository = productsRepository;
        private readonly IGenericRepository<Brand> _brandsRepository = brandsRepository;
        private readonly IGenericRepository<Category> _categoriesRepository = categoriesRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet()]
        public async Task<IActionResult> GetProducts([FromQuery]ProductSpecsParams productParamsModel)
        {
            var products = 
                await _productsRepository.GetAllWithSpecsAsync(new ProductSpecifications(productParamsModel));

            var response = new PaginationResponse<ProductToreturnDTO>()
            {
                Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToreturnDTO>>(products),
                PageIndex = productParamsModel.PageIndex,
                PageSize = productParamsModel.PageSize,
                Count = await _productsRepository.GetCountAsync(new ProductCountSpecs(productParamsModel))
            };

            return Ok(response);

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product =
                await _productsRepository.GetWithSpecsAsync(new ProductSpecifications(id));

            return product is null ? 
                        BadRequest(new {message = "No Product With this id"}) : 
                        Ok(_mapper.Map<Product, ProductToreturnDTO>(product));
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _brandsRepository.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoriesRepository.GetAllAsync();
            return Ok(categories);
        }
    }
}
