using Entities.Models;
using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IFridgeRepository
	{
		IEnumerable<Fridge> GetAllFridges(bool trackChanges);
		public Fridge GetFridge(Guid Id, bool trackChanges);
		public void AddProductToFridge(Guid fridgeId, Product product, int quantity);
		public void RemoveProductFromFridge(Guid fridgeId, Guid productId);

		public (Guid firdgeId, Guid productId) GetFridgeProductWithZeroQuantity();

		Task<IEnumerable<Fridge>> GetAllFridgesAsync(bool trackChanges);
		Task<Fridge> GetFridgeAsync(Guid Id, bool trackChanges);
		Task<bool> AddProductToFridgeAsync(Guid fridgeId, Product product, int quantity);
		Task<bool> UpdateProductInFridgeAsync(Guid fridgeId, Product product, int quantity);
		Task RemoveProductFromFridgeAsync(Guid fridgeId, Guid productId);

		Task<(Guid firdgeId, Guid productId)> GetFridgeProductWithZeroQuantityAsync();

		public void CreateFridge(Fridge entity);
		public void DeleteFridge(Fridge fridge);
	}
}
