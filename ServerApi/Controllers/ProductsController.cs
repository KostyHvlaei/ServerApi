﻿using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Entities.DataTransferObjects;
using Entities.Models;

namespace ServerApi.Controllers
{
	[Route("api/products")]
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
	public class ProductsController : ControllerBase
	{
		private readonly IRepositoryManager _repository;
		private readonly ILoggerManager _logger;

		public ProductsController(IRepositoryManager repository, ILoggerManager logger)
		{
			_repository = repository;
			_logger = logger;
		}

		[HttpGet]
		public IActionResult GetProducts()
		{
			try
			{
				var products = _repository.Product.GetAllProducts(false);
				var productsDTO = products.Select(p => new ProductDTO
				{
					Id = p.Id,
					Name = p.Name,
					DefaultQuantity = p.DefaultQuantity
				}).ToList();

				return Ok(productsDTO);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error in the {nameof(GetProducts)} action {ex}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("{id}", Name = "GetProductById")]
		public IActionResult GetProduct(Guid id)
		{
			var product = _repository.Product.GetProduct(id, false);
			if (product == null)
			{
				_logger.LogInfo($"Product with id {id} doesn't exists");
				return NotFound();
			}
			else
			{
				var productDTO = new ProductDTO { Id = product.Id, Name = product.Name, DefaultQuantity = product.DefaultQuantity };
				return Ok(productDTO);
			}
		}

		[HttpPost]
		public IActionResult CreateProduct([FromBody] ProductToCreationDTO product)
		{
			if (product == null)
			{
				_logger.LogError("FridgeModelToCreationDTO object sent from client is null");
				return BadRequest("FridgeModelToCreationDTO object in null");
			}

			if (!ModelState.IsValid)
			{
				_logger.LogError("Invalid model state for the ProductToCreationDTO object");
				return UnprocessableEntity(ModelState);
			}

			Product product_to_create = new Product { Name = product.Name, DefaultQuantity = product.DefaultQuantity };
			_repository.Product.Create(product_to_create);
			_repository.Save();

			var productDTO = new ProductDTO { Id = product_to_create.Id, Name = product_to_create.Name, DefaultQuantity = product_to_create.DefaultQuantity };

			return CreatedAtRoute("GetProductById", new { id = product_to_create.Id }, productDTO);
		}

		[HttpPut("{productId}")]
		public IActionResult UpdateFridge(Guid productId, [FromBody] ProductToUpdateDTO product)
		{
			if (product == null)
			{
				_logger.LogError("ProductToUpdateDTO object sent from client is null.");
				return BadRequest("ProductToUpdateDTO object is null");
			}

			if (!ModelState.IsValid)
			{
				_logger.LogError("Invalid model state for the ProductToUpdateDTO object");
				return UnprocessableEntity(ModelState);
			}

			var _product = _repository.Product.GetProduct(productId, true);
			if (_product == null)
			{
				_logger.LogInfo($"Product with id: {productId} doesn't exist in the database.");
				return NotFound();
			}

			_product.Name = product.Name;
			_product.DefaultQuantity = product.DefaultQuantity;
			_repository.Save();

			return NoContent();
		}

		[HttpDelete("{productId}")]
		public IActionResult DeleteFridgeModel(Guid productId)
		{
			var product = _repository.Product.GetProduct(productId, false);
			if (product == null)
			{
				_logger.LogInfo($"Product with id: {productId} doesn't exist in the database.");
				return NotFound();
			}

			_repository.Product.DeleteProduct(product);
			_repository.Save();

			return NoContent();
		}
	}
}
