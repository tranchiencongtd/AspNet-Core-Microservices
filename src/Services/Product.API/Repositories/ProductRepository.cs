using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistences;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories
{
  public class ProductRepository : RepositoryBaseAsync<CatalogProduct, long, ProductContext>, IProductRepository
  {
    public ProductRepository(ProductContext dbContext, IUnitOfWork<ProductContext> unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public Task CreateProductAsync(CatalogProduct product) => CreateAsync(product);

    public Task UpdateProductAsync(CatalogProduct product) => UpdateAsync(product);

    public async Task DeleteProductAsync(long id)
    {
      var product = await GetByIdAsync(id);
      if(product != null) DeleteAsync(product); 
    }

    public Task<CatalogProduct> GetProductByIdAsync(long id) => GetByIdAsync(id);
   

    public Task<CatalogProduct> GetProductByNoAsync(string productNo) => FindByCondition(x => x.No.Equals(productNo)).SingleOrDefaultAsync();      

    public async Task<IEnumerable<CatalogProduct>> GetProductsAsync() => await FindAll().ToArrayAsync();  
    
  }
}
