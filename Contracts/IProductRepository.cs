using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IProductRepository
	{
		IEnumerable<Product> GetAllProducts(bool trackChanges);
		public Product GetProduct(Guid productId, bool trackChanges);
		public int? GetDefaultQuantity(Guid prodId);

		Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges);
		Task<Product> GetProductAsync(Guid productId, bool trackChanges);
		Task<int?> GetDefaultQuantityAsync(Guid prodId);

		public void CreateProduct(Product product);
		public void DeleteProduct(Product product);
	}
}
