using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Entities.DataTransferObjects;
using System.Collections.Generic;
using Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Entities;
using System.Threading.Tasks;

namespace ServerApi.Controllers
{
	[Route("api/fridges")]
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
	public class FridgesController : ControllerBase
	{
		private readonly IRepositoryManager _repository;
		private readonly ILoggerManager _logger;

		public FridgesController(IRepositoryManager repository, ILoggerManager logger)
		{
			_repository = repository;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetFridges()
		{
			try
			{
				var fridges = await _repository.Fridge.GetAllFridgesAsync(false);

				var fridgesDTO = fridges.Select(f => new FridgeDTO
				{
					Id = f.Id,
					Name = f.Name,
					OwnerName = f.OwnerName,
					ModelName = f.FridgeModel.Name
				}).ToList();

				return Ok(fridgesDTO);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error in the {nameof(GetFridges)} action {ex}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("{id}", Name = "GetFridgeById")]
		public async Task<IActionResult> GetFrige(Guid id)
		{
			var fridge = await _repository.Fridge.GetFridgeAsync(id, false);
			if (fridge == null)
			{
				_logger.LogInfo($"Fridge with id {id} doesn't exists");
				return NotFound();
			}
			else
			{
				var fridgeDTO = new FridgeProductsDTO
				{
					FridgeId = fridge.Id,
					FridgeName = fridge.Name,
					ModelName = fridge.FridgeModel.Name,
					OwnerName = fridge.OwnerName,
					Products = fridge.FridgeProducts
						.Select(fp => new FridgeProductDTO 
						{ 
							ProductId = fp.ProductId,
							ProductName = fp.Product.Name,
							Quantity = fp.Quantity 
						})
				};
				return Ok(fridgeDTO);
			}
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> UpdateFrigdeProducts()
		{
			int count = 0;
			(var prodId, var fridId) = await _repository.Fridge.GetFridgeProductWithZeroQuantityAsync();


			while(prodId != Guid.Empty && fridId != Guid.Empty)
			{
				int? def_quant = _repository.Product.GetDefaultQuantity(prodId);

				_repository.Save();
				if (def_quant != null && def_quant != 0)
				{
					await AddProductToFridge(fridId, new ProductToAddInFridgeDTO { ProductId = prodId, Quantity = (int)def_quant });
					count++;
				}
				(prodId, fridId) = await _repository.Fridge.GetFridgeProductWithZeroQuantityAsync();
			}

			return Content($"{count} objects updated");
		}

		[HttpPost]
		public async Task<IActionResult> CreateFridge([FromBody] FridgeToCreationDTO fridge)
		{
			if (fridge == null)
			{
				_logger.LogError("FridgeModelToCreationDTO object sent from client is null");
				return BadRequest("FridgeModelToCreationDTO object in null");
			}

			var fridgeModel = await _repository.FridgeModel.GetFridgeModelAsync(fridge.FridgeModelId, false);

			if (fridgeModel == null)
			{
				_logger.LogError($"There is no fridge model with {fridge.FridgeModelId}");
				return BadRequest($"There is no fridge model with {fridge.FridgeModelId}");
			}

			if (!ModelState.IsValid)
			{
				_logger.LogError("Invalid model state for the FridgeToCreationDTO object");
				return UnprocessableEntity(ModelState);
			}

			Fridge fridge_to_create = new Fridge 
			{
				Name = fridge.Name,
				OwnerName = fridge.OwnerName,
				FridgeModelId = fridge.FridgeModelId 
			};
			_repository.Fridge.CreateFridge(fridge_to_create);
			await _repository.SaveAsync();

			var fridgeDTO = new FridgeDTO 
			{ 
				Id = fridge_to_create.Id,
				Name = fridge_to_create.Name, 
				OwnerName = fridge_to_create.OwnerName, 
				ModelName = fridgeModel.Name 
			};

			return CreatedAtRoute("GetFridgeById", new { id = fridge_to_create.Id }, fridgeDTO);
		}


		[HttpPost("{fridgeId}")]
		public async Task<IActionResult> AddProductToFridge(Guid fridgeId, [FromBody] ProductToAddInFridgeDTO productDTO)
		{
			if(productDTO == null)
			{
				_logger.LogError("Product object sent from client is null.");
				return BadRequest("Product object is null");
			}

			if (!ModelState.IsValid)
			{
				_logger.LogError("Invalid model state for the ProductToAddInFridgeDTO object");
				return UnprocessableEntity(ModelState);
			}

			int quantity = productDTO.Quantity;

			var product = await _repository.Product.GetProductAsync(productDTO.ProductId, true);

			if(product == null)
			{
				_logger.LogError("There is no product with this id.");
				return BadRequest("There is no product with this id");
			}
			
			var fridge = await _repository.Fridge.GetFridgeAsync(fridgeId, false);
			if (fridge == null)
			{
				_logger.LogError("There is no fridge with this id.");
				return BadRequest("There is no fridge with this id");
			}

			if(!await _repository.Fridge.AddProductToFridgeAsync(fridgeId, product, quantity))
			{
				return BadRequest("Product with this productId already in fridge with this fridgeId");
			}

			var fridgeDTO = new FridgeProductsDTO
			{
				FridgeId = fridge.Id,
				FridgeName = fridge.Name,
				ModelName = fridge.FridgeModel.Name,
				OwnerName = fridge.OwnerName,
				Products = fridge.FridgeProducts.Select(
					fp => new FridgeProductDTO 
					{
						ProductId = fp.Id,
						ProductName = fp.Product.Name, 
						Quantity = fp.Quantity 
					})
			};

			return CreatedAtRoute("GetFridgeById", new { id = fridge.Id }, fridgeDTO);
		}

		[HttpPut("{fridgeId}/{productId}")]
		public async Task<IActionResult> UpdateProductInFridge(Guid fridgeId, Guid productId, [FromBody] ProductToUpdateInFridgeDTO productDTO)
		{
			if (productDTO == null)
			{
				_logger.LogError("Product object sent from client is null.");
				return BadRequest("Product object is null");
			}

			if (!ModelState.IsValid)
			{
				_logger.LogError("Invalid model state for the ProductToAddInFridgeDTO object");
				return UnprocessableEntity(ModelState);
			}

			var product = await _repository.Product.GetProductAsync(productId, true);

			if (product == null)
			{
				_logger.LogError("There is no product with this id.");
				return BadRequest("There is no product with this id");
			}

			var fridge = await _repository.Fridge.GetFridgeAsync(fridgeId, false);
			if (fridge == null)
			{
				_logger.LogError("There is no fridge with this id.");
				return BadRequest("There is no fridge with this id");
			}

			if(await _repository.Fridge.UpdateProductInFridgeAsync(fridgeId, product, productDTO.Quantity))
			{
				return NoContent();
			}
			return BadRequest("Something went wrong");
			
		}

		[HttpDelete("{fridgeId}/{productId}")]
		public async Task<IActionResult> RemoveProductFromFridge(Guid fridgeId, Guid productId)
		{
			try
			{
				await _repository.Fridge.RemoveProductFromFridgeAsync(fridgeId, productId);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message.ToString());
			}

			return NoContent();
		}

		[HttpPut("{fridgeId}")]
		public async Task<IActionResult> UpdateFridge(Guid fridgeId, [FromBody] FridgeToUpdateDTO fridge)
		{
			if (fridge == null)
			{
				_logger.LogError("FridgeToUpdateDTO object sent from client is null.");
				return BadRequest("FridgeToUpdateDTO object is null");
			}

			if (!ModelState.IsValid)
			{
				_logger.LogError("Invalid model state for the FridgeToUpdateDTO object");
				return UnprocessableEntity(ModelState);
			}

			var _fridge = await _repository.Fridge.GetFridgeAsync(fridgeId, true);
			if (_fridge == null)
			{
				_logger.LogInfo($"Fridge with id: {fridgeId} doesn't exist in the database.");
				return NotFound();
			}

			_fridge.Name = fridge.Name;
			_fridge.OwnerName = fridge.OwnerName;
			await _repository.SaveAsync();

			return NoContent();
		}

		//Error with multi tracking when call from mvc
		[HttpDelete("{fridgeId}")]
		public async Task<IActionResult> DeleteFridge(Guid fridgeId)
		{
			var fridge = await _repository.Fridge.GetFridgeAsync(fridgeId, true);
			if (fridge == null)
			{
				_logger.LogInfo($"Fridge with id: {fridgeId} doesn't exist in the database.");
				return NotFound();
			}
			_repository.Fridge.DeleteFridge(fridge);
			await _repository.SaveAsync();

			return NoContent();
		}
	}
}
