using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServerApi.Tests
{
	public class FridgesControllerTests
	{
		[Fact]
		public async void GetAllFridgesTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();

			repositoryMock.Setup(r => r.Fridge.GetAllFridgesAsync(false))
				.Returns(Task.FromResult(expected));


			var controller = new FridgesController(repositoryMock.Object, loggerMock.Object);

			var result = await controller.GetFridges();
			var objectResult = result as OkObjectResult;
			var list = objectResult.Value as IEnumerable<FridgeDTO>;


			Assert.IsType<OkObjectResult>(result);
			Assert.True(list.Count() == expected.Count());
		}

		[Fact]
		public async void GetFridgeTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var fridge = new Fridge
			{
				Id = new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"),
				Name = "Genady Gorin's fridge",
				OwnerName = "Genady Gorin",
				FridgeModelId = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				FridgeModel = new FridgeModel
				{
					Id = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
					Name = "Model name",
					Year = 2000
				}
			};
			repositoryMock.Setup(r => r.Fridge.GetFridgeAsync(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), false)).Returns(Task.FromResult(fridge));


			var controller = new FridgesController(repositoryMock.Object, loggerMock.Object);

			var resultNF = await controller.GetFrige(Guid.Parse("2fd5ea01-c9e9-4215-b844-fd66e80d3e79"));
			var resultOK = await controller.GetFrige(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"));

			Assert.IsType<NotFoundResult>(resultNF);
			Assert.IsType<OkObjectResult>(resultOK);
		}

		[Fact]
		public async void UpdateFridgeProductsTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();

			repositoryMock.Setup(r => r.Fridge.GetFridgeProductWithZeroQuantityAsync())
				.Returns(Task.FromResult((Guid.Empty, Guid.Empty)));


			var controller = new FridgesController(repositoryMock.Object, loggerMock.Object);
			var result = await controller.UpdateFrigdeProducts();
			var content = result as ContentResult;


			Assert.IsType<ContentResult>(result);
			Assert.Equal("0 objects updated", content.Content);
		}

		[Fact]
		public async void CreateFridgeTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var fridgeTC = new FridgeToCreationDTO
			{
				Name = "Genady Gorin's fridge",
				OwnerName = "Genady Gorin",
				FridgeModelId = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c")
			};
			Fridge fridge_to_create = new Fridge { Name = fridgeTC.Name, OwnerName = fridgeTC.OwnerName, FridgeModelId = fridgeTC.FridgeModelId };
			var fridgeModel = new FridgeModel
			{
				Id = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				Name = "Some name",
				Year = 2000
			};
			repositoryMock.Setup(r => r.FridgeModel.GetFridgeModelAsync(Guid.Parse("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"), false)).Returns(Task.FromResult(fridgeModel));
			repositoryMock.Setup(r => r.Fridge.CreateFridge(fridge_to_create));

			var controller = new FridgesController(repositoryMock.Object, loggerMock.Object);

			var resultBR = await controller.CreateFridge(null);
			var resultOK = await controller.CreateFridge(fridgeTC);
			fridgeTC.FridgeModelId = new Guid("a4793a96-678a-4cae-a6a3-f7cc51a6b98c");
			var resultBR_second = await controller.CreateFridge(fridgeTC);

			fridgeTC.FridgeModelId = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c");
			controller.ModelState.AddModelError("Some", "Error");
			var resultUE = await controller.CreateFridge(fridgeTC);


			Assert.IsType<BadRequestObjectResult>(resultBR);
			Assert.IsType<BadRequestObjectResult>(resultBR_second);
			Assert.IsType<UnprocessableEntityObjectResult>(resultUE);
			Assert.IsType<CreatedAtRouteResult>(resultOK);
		}

		[Fact]
		public async void AddProductToFridgeTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var productToAdd = new ProductToAddInFridgeDTO
			{
				ProductId = new Guid("b4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				Quantity = 10
			};
			var product = new Product
			{
				Id = new Guid("a4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				Name = "Some product",
				DefaultQuantity = 3
			};
			var product_second = new Product
			{
				Id = new Guid("b4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				Name = "Some product",
				DefaultQuantity = 3
			};

			var fridgeModel = new FridgeModel
			{
				Id = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				Name = "Model name",
				Year = 2000
			};
			var fridge = new Fridge
			{
				Id = new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"),
				Name = "Genady Gorin's fridge",
				OwnerName = "Genady Gorin",
				FridgeModelId = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				FridgeModel = fridgeModel,
				Products = new List<Product>(),
				FridgeProducts = new List<FridgeProduct>
				{
					new FridgeProduct
					{
						Id = Guid.NewGuid(),
						ProductId = new Guid("a4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
						FridgeId = new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"),
						Quantity = 10
					}
				}
			};

			repositoryMock.Setup(r => r.Product.GetProductAsync(Guid.Parse("a4793a96-678a-4cae-a6a3-f7cc51a6b98c"), true))
				.Returns(Task.FromResult(product));
			repositoryMock.Setup(r => r.Product.GetProductAsync(Guid.Parse("b4793a96-678a-4cae-a6a3-f7cc51a6b98c"), true))
				.Returns(Task.FromResult(product_second));

			repositoryMock.Setup(r => r.Fridge.GetFridgeAsync(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), false))
				.Returns(Task.FromResult(fridge));

			repositoryMock.Setup(r => r.Fridge.AddProductToFridgeAsync(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), product, 10))
				.Returns(Task.FromResult(false));
			repositoryMock.Setup(r => r.Fridge.AddProductToFridgeAsync(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), product_second, 10))
				.Returns(Task.FromResult(true));

	

			var controller = new FridgesController(repositoryMock.Object, loggerMock.Object);

			var resultBR = await controller.AddProductToFridge(Guid.Empty, null);
			var resultOK = await controller.AddProductToFridge(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), productToAdd);

			productToAdd.ProductId = new Guid("a4793a96-678a-4cae-a6a3-f7cc51a6b98c");
			var resultBR_second = await controller.AddProductToFridge(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), productToAdd);

			var resultBR_third = await controller.AddProductToFridge(Guid.Empty, productToAdd);

			controller.ModelState.AddModelError("Some", "Error");
			var resultUE = await controller.AddProductToFridge(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), productToAdd);


			Assert.IsType<BadRequestObjectResult>(resultBR);
			Assert.IsType<BadRequestObjectResult>(resultBR_second);
			Assert.IsType<BadRequestObjectResult>(resultBR_third);
			Assert.IsType<UnprocessableEntityObjectResult>(resultUE);
			Assert.IsType<CreatedAtRouteResult>(resultOK);
		}

		[Fact]
		public async void UpdateProductInFridgeTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var productToUpdate = new ProductToUpdateInFridgeDTO
			{
				Quantity = 10
			};
			var product = new Product
			{
				Id = new Guid("a4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				Name = "Some product",
				DefaultQuantity = 3
			};
			var product_second = new Product
			{
				Id = new Guid("b4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				Name = "Some product",
				DefaultQuantity = 3
			};

			var fridgeModel = new FridgeModel
			{
				Id = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				Name = "Model name",
				Year = 2000
			};
			var fridge = new Fridge
			{
				Id = new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"),
				Name = "Genady Gorin's fridge",
				OwnerName = "Genady Gorin",
				FridgeModelId = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				FridgeModel = fridgeModel,
				Products = new List<Product>(),
				FridgeProducts = new List<FridgeProduct>
				{
					new FridgeProduct
					{
						Id = Guid.NewGuid(),
						ProductId = new Guid("a4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
						FridgeId = new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"),
						Quantity = 10
					}
				}
			};


			repositoryMock.Setup(r => r.Product.GetProductAsync(Guid.Parse("a4793a96-678a-4cae-a6a3-f7cc51a6b98c"), true))
				.Returns(Task.FromResult(product));
			repositoryMock.Setup(r => r.Product.GetProductAsync(Guid.Parse("b4793a96-678a-4cae-a6a3-f7cc51a6b98c"), true))
				.Returns(Task.FromResult(product_second));

			repositoryMock.Setup(r => r.Fridge.GetFridgeAsync(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), false))
				.Returns(Task.FromResult(fridge));

			repositoryMock.Setup(r => r.Fridge.UpdateProductInFridgeAsync(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), product, 10))
				.Returns(Task.FromResult(false));
			repositoryMock.Setup(r => r.Fridge.UpdateProductInFridgeAsync(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), product_second, 10))
				.Returns(Task.FromResult(true));


			var controller = new FridgesController(repositoryMock.Object, loggerMock.Object);

			var resultBR = await controller.UpdateProductInFridge(Guid.Empty, Guid.Empty, null);
			var resultOK = await controller.UpdateProductInFridge(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"),
				new Guid("b4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				productToUpdate);

			var resultBR_third = await controller.UpdateProductInFridge(Guid.Empty, new Guid("b4793a96-678a-4cae-a6a3-f7cc51a6b98c"), productToUpdate);

			controller.ModelState.AddModelError("Some", "Error");
			var resultUE = await controller.UpdateProductInFridge(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), 
				new Guid("b4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				productToUpdate);


			Assert.IsType<BadRequestObjectResult>(resultBR);
			Assert.IsType<BadRequestObjectResult>(resultBR_third);
			Assert.IsType<UnprocessableEntityObjectResult>(resultUE);
			Assert.IsType<NoContentResult>(resultOK);
		}

		[Fact]
		public async void RemoveProductFromFridgeTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			repositoryMock.Setup(r => r.Fridge.RemoveProductFromFridgeAsync(Guid.Empty, Guid.Empty)).Throws<ArgumentException>();

			var controller = new FridgesController(repositoryMock.Object, loggerMock.Object);

			var resultBR = await controller.RemoveProductFromFridge(Guid.Empty, Guid.Empty);

			var resultOK = await controller.RemoveProductFromFridge(Guid.NewGuid(), Guid.NewGuid());

			Assert.IsType<BadRequestObjectResult>(resultBR);
			Assert.IsType<NoContentResult>(resultOK);
		}

		[Fact]
		public async void UpdateFridgeTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var fridge = new Fridge
			{
				Id = new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"),
				Name = "Genady Gorin's fridge",
				OwnerName = "Genady Gorin",
				FridgeModelId = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c")
			};
			var fridgeToUpdate = new FridgeToUpdateDTO
			{
				Name = "Genady Gorin's fridge",
				OwnerName = "New Name",
			};
			repositoryMock.Setup(r => r.Fridge.GetFridgeAsync(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), true))
				.Returns(Task.FromResult(fridge));


			var controller = new FridgesController(repositoryMock.Object, loggerMock.Object);

			var resultBR = await controller.UpdateFridge(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), null);
			var resultNF = await controller.UpdateFridge(Guid.Parse("2fd5ea01-c9e9-4215-b844-fd66e80d3e79"), fridgeToUpdate);
			var resultOK = await controller.UpdateFridge(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), fridgeToUpdate);

			controller.ModelState.AddModelError("Some", "Error");
			var resultUE = await controller.UpdateFridge(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), fridgeToUpdate);


			Assert.IsType<BadRequestObjectResult>(resultBR);
			Assert.IsType<UnprocessableEntityObjectResult>(resultUE);
			Assert.IsType<NotFoundResult>(resultNF);
			Assert.IsType<NoContentResult>(resultOK);
		}

		[Fact]
		public async void DeleteFridgeTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var fridge = new Fridge
			{
				Id = new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"),
				Name = "Genady Gorin's fridge",
				OwnerName = "Genady Gorin",
				FridgeModelId = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c")
			};
			repositoryMock.Setup(r => r.Fridge.GetFridgeAsync(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), true))
				.Returns(Task.FromResult(fridge));


			var controller = new FridgesController(repositoryMock.Object, loggerMock.Object);

			var resultNF = await controller.DeleteFridge(Guid.Empty);
			var resultOK = await controller.DeleteFridge(Guid.Parse("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"));


			Assert.IsType<NotFoundResult>(resultNF);
			Assert.IsType<NoContentResult>(resultOK);
		}

		private IEnumerable<Fridge> expected = new List<Fridge>
		{
			new Fridge
				{
					Id = new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"),
					Name = "Genady Gorin's fridge",
					OwnerName = "Genady Gorin",
					FridgeModelId = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
					FridgeModel = new FridgeModel
					{
						Id = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
						Name = "Model name",
						Year = 2000
					}
				},
				new Fridge
				{
					Id = new Guid("ae16b99d-85f3-4121-bdad-e704c29e3981"),
					Name = "Kitchen",
					OwnerName = "Some guy",
					FridgeModelId = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
					FridgeModel = new FridgeModel
					{
						Id = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
						Name = "Model name",
						Year = 2000
					}
				},
				new Fridge
				{
					Id = new Guid("d447d5c7-3d61-495c-8d36-89c337e3a9ef"),
					Name = "Stolovaya n2",
					FridgeModelId = new Guid("24964f45-61f7-4f9c-9ab8-c4981ad6a252"),
					FridgeModel = new FridgeModel
					{
						Id = new Guid("24964f45-61f7-4f9c-9ab8-c4981ad6a252"),
						Name = "Model name",
						Year = 2000
					}
				},
				new Fridge
				{
					Id = new Guid("8be43fc6-4398-4714-8794-edacee214946"),
					Name = "Stolovaya n3",
					FridgeModelId = new Guid("24964f45-61f7-4f9c-9ab8-c4981ad6a252"),
					FridgeModel = new FridgeModel
					{
						Id = new Guid("24964f45-61f7-4f9c-9ab8-c4981ad6a252"),
						Name = "Model name",
						Year = 2000
					}
				},
				new Fridge
				{
					Id = new Guid("92bafc65-cc77-485c-9756-81ad0aaa8008"),
					Name = "Mine fridge",
					OwnerName = "Man who wants to become developer",
					FridgeModelId = new Guid("e3130a06-9410-44c0-bc38-6145b5e60426"),
					FridgeModel = new FridgeModel
					{
						Id = new Guid("e3130a06-9410-44c0-bc38-6145b5e60426"),
						Name = "Model name",
						Year = 2000
					}
				}
		};
	}
}
