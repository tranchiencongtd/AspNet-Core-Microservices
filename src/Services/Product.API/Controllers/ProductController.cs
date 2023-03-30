using AutoMapper;
using Contracts.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistences;
using Product.API.Repositories.Interfaces;
using Shared.DTOs.Product;
using System.ComponentModel.DataAnnotations;

namespace Product.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductController : ControllerBase
  {
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductController(IProductRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
      var products = await _repository.GetProductsAsync();
      var result = _mapper.Map<IEnumerable<ProductDto>>(products);
      return Ok(result);
    }

    #region CRUD

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
      var productEntity = await _repository.GetProductByNoAsync(productDto.No);
      if (productEntity != null) return BadRequest($"Product no: {productDto.No} is existed");

      var product = _mapper.Map<CatalogProduct>(productDto);
      await _repository.CreateAsync(product);
      await _repository.SaveChangesAsync();
      return Ok(product);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateProduct([Required] long id, [FromBody] UpdateProductDto productDto)
    {
      var product = await _repository.GetProductByIdAsync(id);
      if(product == null)
      {
        return NotFound();
      }

      var updateProduct = _mapper.Map(productDto, product);
      await _repository.UpdateProductAsync(updateProduct);
      await _repository.SaveChangesAsync();
      var result = _mapper.Map<ProductDto>(product);
      return Ok(result);  
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteProduct([Required] long id)
    {
      var product = await _repository.GetProductByIdAsync(id);
      if (product == null)
        return NotFound();

      await _repository.DeleteAsync(product);
      await _repository.SaveChangesAsync();

      return NoContent();
    }

    #endregion

    #region AddResources
    [HttpGet("get-product-by-no/{productNo}")]
    public async Task<IActionResult> GetProductByNo([Required] string productNo)
    {
      var product = await _repository.GetProductByNoAsync(productNo);
      if (product == null)
      {
        return NotFound();
      }

      var result = _mapper.Map<ProductDto>(product);
      return Ok(result);
    }

    #endregion
  }
}