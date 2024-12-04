using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories
{
    public class ProductRepository : RepositoryBaseAsync<CatalogProduct, long, ProductDbContext>, IProductRepository
    {
        public ProductRepository(ProductDbContext context, IUnitOfWork<ProductDbContext> unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<IEnumerable<CatalogProduct>> GetProducts()
        {
            return await FindAll().ToListAsync();
        }

        public Task<CatalogProduct> GetProduct(long id)
        {
            return GetByIdAsync(id);
        }

        public Task<CatalogProduct> GetProductByNo(string productNo)
        {
            return FindByCondition(x => x.No.Equals(productNo)).SingleOrDefaultAsync();
        }

        public Task CreateProduct(CatalogProduct product)
        {
            return CreateAsync(product);
        }

        public Task UpdateProduct(CatalogProduct product)
        {
            return UpdateAsync(product);
        }

        public async Task DeleteProduct(long id)
        {
            var product = await GetProduct(id);
            if (product != null) 
            {
                await DeleteAsync(product);
            }
        }
    }
}
