﻿using Contracts.Common.Interfaces;
using Product.API.Entities;
using Product.API.Persistences;

namespace Product.API.Repositories.Interfaces
{
  public interface IProductRepository : IRepositoryBaseAsync<CatalogProduct, long, ProductContext>
  {
    Task<IEnumerable<CatalogProduct>> GetProductsAsync();
    Task<CatalogProduct> GetProductByIdAsync(long id);
    Task<CatalogProduct> GetProductByNoAsync(string productNo);
    Task CreateProductAsync(CatalogProduct product);
    Task UpdateProductAsync(CatalogProduct product);
    Task DeleteProductAsync(long id);
  }
}
