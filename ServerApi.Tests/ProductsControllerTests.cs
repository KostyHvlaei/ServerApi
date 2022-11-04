using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Contracts;
using Entities.Models;
using ServerApi.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;

namespace ServerApi.Tests
{
	public class ProductsControllerTests
	{
		[Fact]
		public async void GetAllProductsTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var envMock = new Mock<IWebHostEnvironment>();
			repositoryMock.Setup(r => r.Product.GetAllProductsAsync(false))
				.Returns(Task.FromResult(expected));


			var controller = new ProductsController(repositoryMock.Object, loggerMock.Object, envMock.Object);

			var result = await controller.GetProducts();
			var objectResult = result as OkObjectResult;
			var list = objectResult.Value as IEnumerable<ProductDTO>;

			Assert.IsType<OkObjectResult>(result);
			Assert.True(list.Count() == expected.Count());
		}

		[Fact]
		public async void GetProductTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var envMock = new Mock<IWebHostEnvironment>();
			var product = new Product
			{
				Id = new Guid("94fea888-0773-4d71-988a-32185bf61eee"),
				Name = "Pizza",
				DefaultQuantity = 6
			};
			repositoryMock.Setup(r => r.Product.GetProductAsync(Guid.Parse("94fea888-0773-4d71-988a-32185bf61eee"), false)).Returns(Task.FromResult(product));


			var controller = new ProductsController(repositoryMock.Object, loggerMock.Object, envMock.Object);

			var resultNF = await controller.GetProduct(Guid.Parse("44fea888-0773-4d71-988a-32185bf61eee"));
			var resultOk = await controller.GetProduct(Guid.Parse("94fea888-0773-4d71-988a-32185bf61eee"));


			Assert.IsType<NotFoundResult>(resultNF);
			Assert.IsType<OkObjectResult>(resultOk);
		}

		[Fact]
		public async void CreateProductTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var envMock = new Mock<IWebHostEnvironment>();
			var product = new Product
			{
				Name = "Pizza",
				DefaultQuantity = 6
			};
			var validProductDTO = new ProductToCreationDTO
			{
				Name = "Pizza",
				DefaultQuantity = 6
			};

			repositoryMock.Setup(r => r.Product.CreateProduct(product));


			var controller = new ProductsController(repositoryMock.Object, loggerMock.Object, envMock.Object);

			var resultBR = await controller.CreateProduct(null);
			var resultOK = await controller.CreateProduct(validProductDTO);
			controller.ModelState.AddModelError("Some", "Error");
			var resultUE = await controller.CreateProduct(validProductDTO);
			

			Assert.IsType<BadRequestObjectResult>(resultBR);
			Assert.IsType<CreatedAtRouteResult>(resultOK);
			Assert.IsType<UnprocessableEntityObjectResult>(resultUE);
		}

		[Fact]
		public async void UpdateProductTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var envMock = new Mock<IWebHostEnvironment>();
			var product = new Product
			{
				Name = "Pizza",
				DefaultQuantity = 6
			};
			var validProductDTO = new ProductToUpdateDTO
			{
				Name = "Pizza",
				DefaultQuantity = 6
			};

			repositoryMock.Setup(r => r.Product.GetProductAsync(Guid.Parse("94fea888-0773-4d71-988a-32185bf61eee"), true))
				.Returns(Task.FromResult(product));


			var controller = new ProductsController(repositoryMock.Object, loggerMock.Object, envMock.Object);

			var resultBR = await controller.UpdateProduct(Guid.Parse("94fea888-0773-4d71-988a-32185bf61eee") , null);
			var resultOK = await controller.UpdateProduct(Guid.Parse("94fea888-0773-4d71-988a-32185bf61eee"), validProductDTO);
			var resultNF = await controller.UpdateProduct(Guid.Parse("44fea888-0773-4d71-988a-32185bf61eee"), validProductDTO);
			controller.ModelState.AddModelError("Some", "Error");
			var resultUE = await controller.UpdateProduct(Guid.Parse("94fea888-0773-4d71-988a-32185bf61eee"), validProductDTO);


			Assert.IsType<BadRequestObjectResult>(resultBR);
			Assert.IsType<NoContentResult>(resultOK);
			Assert.IsType<NotFoundResult>(resultNF);
			Assert.IsType<UnprocessableEntityObjectResult>(resultUE);
		}

		[Fact]
		public async void DeleteProductTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var envMock = new Mock<IWebHostEnvironment>();
			var product = new Product
			{
				Name = "Pizza",
				DefaultQuantity = 6
			};
			var validProductDTO = new ProductToUpdateDTO
			{
				Name = "Pizza",
				DefaultQuantity = 6
			};

			repositoryMock.Setup(r => r.Product.GetProductAsync(Guid.Parse("94fea888-0773-4d71-988a-32185bf61eee"), false))
				.Returns(Task.FromResult(product));


			var controller = new ProductsController(repositoryMock.Object, loggerMock.Object, envMock.Object);

			var resultNF = await controller.DeleteProduct(Guid.Parse("44fea888-0773-4d71-988a-32185bf61eee"));
			var resultOK = await controller.DeleteProduct(Guid.Parse("94fea888-0773-4d71-988a-32185bf61eee"));


			Assert.IsType<NotFoundResult>(resultNF);
			Assert.IsType<NoContentResult>(resultOK);
		}

		private IEnumerable<Product> expected = new List<Product>
		{
			new Product
				{
					Id = new Guid("94fea888-0773-4d71-988a-32185bf61eee"),
					Name = "Pizza",
					DefaultQuantity = 6
				},
				new Product
				{
					Id = new Guid("297a1295-eb00-4447-bb88-5d9b69a1e1f1"),
					Name = "Juice",
					DefaultQuantity = 1
				},
				new Product
				{
					Id = new Guid("70497196-fb2d-4e29-8f17-1c2068afd916"),
					Name = "Apple",
					DefaultQuantity = 10
				},
				new Product
				{
					Id = new Guid("e5d96170-0301-459b-96f4-795a65783654"),
					Name = "Carrot",
					DefaultQuantity = 10
				},
				new Product
				{
					Id = new Guid("29f71e4f-161a-4dab-86af-e2f7aec1e2ba"),
					Name = "Chicken",
					DefaultQuantity = 2
				}
		};
	}
}
