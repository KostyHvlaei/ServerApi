using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Entities.DataTransferObjects;
using Entities.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.IO.Pipes;

namespace ServerApi.Controllers
{
	[Route("api/products")]
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
	public class ProductsController : ControllerBase
	{
		private readonly IRepositoryManager _repository;
		private readonly ILoggerManager _logger;
		private readonly IWebHostEnvironment _environment;

		public ProductsController(IRepositoryManager repository, ILoggerManager logger, IWebHostEnvironment environment)
		{
			_repository = repository;
			_logger = logger;
			_environment = environment;
		}

		[HttpGet]
		public async Task<IActionResult> GetProducts()
		{
			try
			{
				var products = await _repository.Product.GetAllProductsAsync(false);
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
		public async Task<IActionResult> GetProduct(Guid id)
		{
			var product = await _repository.Product.GetProductAsync(id, false);
			if (product == null)
			{
				_logger.LogInfo($"Product with id {id} doesn't exists");
				return NotFound();
			}
			else
			{
				var productDTO = new ProductDTO 
				{
					Id = product.Id,
					Name = product.Name,
					DefaultQuantity = product.DefaultQuantity 
				};
				return Ok(productDTO);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct([FromBody] ProductToCreationDTO product)
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

			Product product_to_create = new Product
			{ 
				Name = product.Name,
				DefaultQuantity = product.DefaultQuantity
			};
			_repository.Product.CreateProduct(product_to_create);
			await _repository.SaveAsync();

			var productDTO = new ProductDTO 
			{
				Id = product_to_create.Id,
				Name = product_to_create.Name, 
				DefaultQuantity = product_to_create.DefaultQuantity 
			};

			return CreatedAtRoute("GetProductById", new { id = product_to_create.Id }, productDTO);
		}

		[HttpPost("uploadimage")]
		public async Task<IActionResult> UploadProductImage([FromForm] ImageUploadDTO imageDTO)
		{
			var prod = await _repository.Product.GetProductAsync(imageDTO.ProductId, false);
			if(prod == null)
			{
				_logger.LogError($"There is no product with this {imageDTO.ProductId}");
				return BadRequest($"There is no product with this {imageDTO.ProductId}");
			}

			if(imageDTO.file.Length == 0)
			{
				_logger.LogError($"There is no file in request");
				return BadRequest($"There is no file in request");
			}

			try
			{
				if (!Directory.Exists(Path.Combine(_environment.WebRootPath, "Images")))
				{
					Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, "Images"));
				}
				using (FileStream fileStream = System.IO.File.Create(
					Path.Combine(_environment.WebRootPath,
					"Images", imageDTO.ProductId.ToString().ToLower()
					+ imageDTO.file.FileName.Substring(imageDTO.file.FileName.LastIndexOf('.')))))
				{
					imageDTO.file.CopyTo(fileStream);
					fileStream.Flush();
				}
			}
			catch (Exception ex)
			{
				return NotFound(ex.ToString());
			}

			return Ok();
		}

		[HttpPut("{productId}")]
		public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] ProductToUpdateDTO product)
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

			var _product = await _repository.Product.GetProductAsync(productId, true);
			if (_product == null)
			{
				_logger.LogInfo($"Product with id: {productId} doesn't exist in the database.");
				return NotFound();
			}

			_product.Name = product.Name;
			_product.DefaultQuantity = product.DefaultQuantity;
			await _repository.SaveAsync();

			return NoContent();
		}

		[HttpDelete("{productId}")]
		public async Task<IActionResult> DeleteProduct(Guid productId)
		{
			var product = await _repository.Product.GetProductAsync(productId, false);
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
