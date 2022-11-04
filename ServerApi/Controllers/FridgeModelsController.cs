using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApi.Controllers
{
	[Route("api/models")]
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
	public class FridgeModelsController : ControllerBase
	{
		private readonly IRepositoryManager _repository;
		private readonly ILoggerManager _logger;

		public FridgeModelsController(IRepositoryManager repository, ILoggerManager logger)
		{
			_repository = repository;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult> GetFridgeModels()
		{
			try
			{
				var fridgemodels = await _repository.FridgeModel.GetAllFridgeModelsAsync(false);
				var fridgemodelsDTO = fridgemodels.Select(fm => new FridgeModelDTO
				{
					Id = fm.Id,
					Name = fm.Name,
					Year = fm.Year
				}).ToList();

				return Ok(fridgemodelsDTO);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error in the {nameof(GetFridgeModels)} action {ex}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("{id}", Name = "FridgeById")]
		public async Task<ActionResult> GetFrigeModel(Guid id)
		{
			var fridgemodel = await _repository.FridgeModel.GetFridgeModelAsync(id, false);

			if(fridgemodel == null)
			{
				_logger.LogInfo($"Fridge with id {id} doesn't exists");
				return NotFound();
			}
			else
			{
				var fridgemodelDTO = new FridgeModelDTO 
				{ 
					Id = fridgemodel.Id,
					Name = fridgemodel.Name,
					Year = fridgemodel.Year 
				};
				return Ok(fridgemodelDTO);
			}
		}

		[HttpPost]
		public async Task<ActionResult> CreateFridgeModel([FromBody]FridgeModelToCreationDTO fridgeModel)
		{
			if(fridgeModel == null)
			{
				_logger.LogError("FridgeModelToCreationDTO object sent from client is null");
				return BadRequest("FridgeModelToCreationDTO object in null");
			}

			if (!ModelState.IsValid)
			{
				_logger.LogError("Invalid model state for the FridgeModelToCreationDTO object");
				return UnprocessableEntity(ModelState);
			}

			FridgeModel fridge = new FridgeModel { Name = fridgeModel.Name, Year = fridgeModel.Year };
			_repository.FridgeModel.CreateFridgeModel(fridge);
			await _repository.SaveAsync();

			var fridgeModelDTO = new FridgeModelDTO { Id = fridge.Id, Name= fridge.Name, Year = fridge.Year };

			return CreatedAtRoute("FridgeById", new { id = fridgeModelDTO.Id }, fridgeModelDTO);
		}

		[HttpPut("{fridgeModelId}")]
		public async Task<ActionResult> UpdateFridgeModel(Guid fridgeModelId, [FromBody] FridgeModelToUpdateDTO fridgeModel)
		{
			if(fridgeModel == null)
			{
				_logger.LogError("FridgeModelToUpdateDTO object sent from client is null.");
				return BadRequest("FridgeModelToUpdateDTO object is null");
			}

			if (!ModelState.IsValid)
			{
				_logger.LogError("Invalid model state for the FridgeModelToUpdateDTO object");
				return UnprocessableEntity(ModelState);
			}

			var fm = await _repository.FridgeModel.GetFridgeModelAsync(fridgeModelId, true);
			if (fm == null)
			{
				_logger.LogInfo($"Fridge with id: {fridgeModelId} doesn't exist in the database.");
				return NotFound();
			}

			fm.Name = fridgeModel.Name;
			fm.Year = fridgeModel.Year;
			await _repository.SaveAsync();

			return NoContent();
		}

		[HttpDelete("{fridgeModelId}")]
		public async Task<ActionResult> DeleteFridgeModel(Guid fridgeModelId)
		{
			var fridgeModel = await _repository.FridgeModel.GetFridgeModelAsync(fridgeModelId, false);
			if(fridgeModel == null)
			{
				_logger.LogInfo($"Fridge with id: {fridgeModelId} doesn't exist in the database.");
				return NotFound();
			}

			_repository.FridgeModel.DeleteFridgeModel(fridgeModel);
			await _repository.SaveAsync();

			return NoContent();
		}
	}
}
