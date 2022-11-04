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
	public class FridgeModelRepository : RepositoryBase<FridgeModel>, IFridgeModelRepository
	{
		public FridgeModelRepository(RepositoryContext repositoryContext)
			: base(repositoryContext)
		{
		}

		public IEnumerable<FridgeModel> GetAllFridgeModels(bool trackChanges) =>
			FindAll(trackChanges)
			.OrderBy(fm => fm.Name)
			.ToList();

		public FridgeModel GetFridgeModel(Guid fridgeModelId, bool trackChanges) =>
			FindByCondition(fm => fm.Id.Equals(fridgeModelId), trackChanges)
			.SingleOrDefault();

		public async Task<IEnumerable<FridgeModel>> GetAllFridgeModelsAsync(bool trackChanges) =>
			await FindAll(trackChanges)
			.OrderBy(fm => fm.Name)
			.ToListAsync();

		public async Task<FridgeModel> GetFridgeModelAsync(Guid fridgeModelId, bool trackChanges) =>
			await FindByCondition(fm => fm.Id.Equals(fridgeModelId), trackChanges)
			.SingleOrDefaultAsync();

		public void CreateFridgeModel(FridgeModel fridgeModel) => Create(fridgeModel);

		public void DeleteFridgeModel(FridgeModel fridge) => Delete(fridge);
	}
}
