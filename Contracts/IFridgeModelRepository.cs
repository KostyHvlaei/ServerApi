using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IFridgeModelRepository
	{

		IEnumerable<FridgeModel> GetAllFridgeModels(bool trackChanges);
		public FridgeModel GetFridgeModel(Guid fridgeModelId, bool trackChanges);

		Task<IEnumerable<FridgeModel>> GetAllFridgeModelsAsync(bool trackChanges);
		Task<FridgeModel> GetFridgeModelAsync(Guid fridgeModelId, bool trackChanges);

		void CreateFridgeModel(FridgeModel fridgeModel);
		public void DeleteFridgeModel(FridgeModel fridgeModel);
	}
}
