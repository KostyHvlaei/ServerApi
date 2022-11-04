using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class ProductRepository : RepositoryBase<Product>, IProductRepository
	{
		private readonly RepositoryContext context;
		public ProductRepository(RepositoryContext repositoryContext)
			: base(repositoryContext)
		{
			context = repositoryContext;
		}

		public IEnumerable<Product> GetAllProducts(bool trackChanges) =>
			FindAll(trackChanges)
			.OrderBy(p => p.Name)
			.ToList();

		public async Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges) =>
			await FindAll(trackChanges)
			.OrderBy(p => p.Name)
			.ToListAsync();

		public Product GetProduct(Guid productId, bool trackChanges) =>
			FindByCondition(f => f.Id.Equals(productId), trackChanges)
			.Include(p => p.Fridges)
			.SingleOrDefault();

		public async Task<Product> GetProductAsync(Guid productId, bool trackChanges) =>
			await FindByCondition(f => f.Id.Equals(productId), trackChanges)
			.Include(p => p.Fridges)
			.SingleOrDefaultAsync();

		public int? GetDefaultQuantity(Guid prodId)
		{
			Product prod = FindByCondition(p => p.Id == prodId, false)
				.FirstOrDefault();
			context.ChangeTracker.Clear();
			return prod?.DefaultQuantity;
		}

		public async Task<int?> GetDefaultQuantityAsync(Guid prodId)
		{
			Product prod = await FindByCondition(p => p.Id == prodId, false)
				.FirstOrDefaultAsync();
			context.ChangeTracker.Clear();
			return prod?.DefaultQuantity;
		}

		public void CreateProduct(Product product) => Create(product);

		public void DeleteProduct(Product product) => Delete(product);
	}
}
