using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Core.IServices;
using Talabat.Core.Specifications.ProductSpecs;
using Talabat.Dtos;
using Talabat.Helpers;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(
        IProductService productService,
        IMapper mapper) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly IMapper _mapper = mapper;

        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetProducts([FromQuery]ProductSpecsParams productParamsModel)
        {
            var products =
                await _productService.GetProductsAsync(productParamsModel);

            var response = new PaginationResponse<ProductToreturnDTO>()
            {
                Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToreturnDTO>>(products),
                PageIndex = productParamsModel.PageIndex,
                PageSize = productParamsModel.PageSize,
                Count = await _productService.GetCountAsync(productParamsModel)
            };

            return Ok(response);

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product =
                await _productService.GetProductAsync(id);

            return product is null ? 
                        BadRequest(new {message = "No Product With this id"}) : 
                        Ok(_mapper.Map<Product, ProductToreturnDTO>(product));
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _productService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _productService.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}
