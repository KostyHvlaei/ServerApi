using System;
using Xunit;
using Moq;
using Contracts;
using System.Threading.Tasks;
using System.Collections.Generic;
using Entities.Models;
using ServerApi.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;

namespace ServerApi.Tests
{
	public class FridgeModelControllerTests
	{
		[Fact]
		public async void GetAllFridgeModelsTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			repositoryMock.Setup(r => r.FridgeModel.GetAllFridgeModelsAsync(false)).Returns(Task.FromResult(expected));

			var controller = new FridgeModelsController(repositoryMock.Object, loggerMock.Object);

			var result = await controller.GetFridgeModels();
			var objectResult = result as OkObjectResult;
			var list = objectResult.Value as IEnumerable<FridgeModelDTO>;


			Assert.IsType<OkObjectResult>(result);
			Assert.True(list.Count() == expected.Count());
		}

		[Fact]
		public async void GetFridgeModelTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var fridgeModel = new FridgeModel
			{
				Id = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				Name = "Atalant b100",
				Year = 2005
			};
			repositoryMock.Setup(r => r.FridgeModel.GetFridgeModelAsync(Guid.Parse("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"), false)).Returns(Task.FromResult(fridgeModel));

			var controller = new FridgeModelsController(repositoryMock.Object, loggerMock.Object);

			var resultOk = await controller.GetFrigeModel(Guid.Parse("f4793a96-678a-4cae-a6a3-f7cc51a6b98c")) as OkObjectResult;
			var resultNF = await controller.GetFrigeModel(Guid.Parse("a4793a96-678a-4cae-a6a3-f7cc51a6b98c"));

			Assert.Equal(fridgeModel.Name, ((FridgeModelDTO)resultOk.Value).Name);
			Assert.IsType<NotFoundResult>(resultNF);
		}

		[Fact]
		public async void CreateFridgeModelTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var validFridgeModelDTO = new FridgeModelToCreationDTO
			{
				Name = "Some name",
				Year = 2000
			};
			var fridgeModel = new FridgeModel
			{
				Name = "Some name",
				Year = 2000
			};

			repositoryMock.Setup(r => r.FridgeModel.CreateFridgeModel(fridgeModel));


			var controller = new FridgeModelsController(repositoryMock.Object, loggerMock.Object);

			var resultBR = await controller.CreateFridgeModel(null);
			var resultOk = await controller.CreateFridgeModel(validFridgeModelDTO);

			controller.ModelState.AddModelError("Some", "Error");
			var resultUE = await controller.CreateFridgeModel(validFridgeModelDTO);


			Assert.IsType<BadRequestObjectResult>(resultBR);
			Assert.IsType<CreatedAtRouteResult>(resultOk);
			Assert.IsType<UnprocessableEntityObjectResult>(resultUE);
		}

		[Fact]
		public async void UpdateFridgeModelTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var updateFridgeModelDTO = new FridgeModelToUpdateDTO
			{
				Name = "Some name",
				Year = 2000
			};
			var fridgeModel = new FridgeModel
			{
				Id = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				Name = "Atalant b100",
				Year = 2005
			};
			repositoryMock.Setup(r => r.FridgeModel.GetFridgeModelAsync(Guid.Parse("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"), true)).Returns(Task.FromResult(fridgeModel));


			var controller = new FridgeModelsController(repositoryMock.Object, loggerMock.Object);

			var resultBR = await controller.UpdateFridgeModel(Guid.Parse("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"), null);
			var resultOk = await controller.UpdateFridgeModel(Guid.Parse("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"), updateFridgeModelDTO);
			var resultNF = await controller.UpdateFridgeModel(Guid.Parse("a4793a96-678a-4cae-a6a3-f7cc51a6b98c"), updateFridgeModelDTO);
			controller.ModelState.AddModelError("Some", "Error");
			var resultUE = await controller.UpdateFridgeModel(Guid.Parse("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"), updateFridgeModelDTO);

			Assert.IsType<BadRequestObjectResult>(resultBR);
			Assert.IsType<NotFoundResult>(resultNF);
			Assert.IsType<NoContentResult>(resultOk);
			Assert.IsType<UnprocessableEntityObjectResult>(resultUE);
		}

		[Fact]
		public async void DeleteFridgeModelTests()
		{
			var repositoryMock = new Mock<IRepositoryManager>();
			var loggerMock = new Mock<ILoggerManager>();
			var fridgeModel = new FridgeModel
			{
				Id = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
				Name = "Atalant b100",
				Year = 2005
			};
			repositoryMock.Setup(r => r.FridgeModel.GetFridgeModelAsync(Guid.Parse("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"), false)).Returns(Task.FromResult(fridgeModel));


			var controller = new FridgeModelsController(repositoryMock.Object, loggerMock.Object);

			var resultNF = await controller.DeleteFridgeModel(Guid.Parse("a4793a96-678a-4cae-a6a3-f7cc51a6b98c"));
			var resultOK = await controller.DeleteFridgeModel(Guid.Parse("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"));


			Assert.IsType<NotFoundResult>(resultNF);
			Assert.IsType<NoContentResult>(resultOK);
		}


		private IEnumerable<FridgeModel> expected = new List<FridgeModel>
		{
			new FridgeModel
				{
					Id = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
					Name = "Atalant b100",
					Year = 2005
				},
				new FridgeModel
				{
					Id = new Guid("b6138124-9e39-4aaf-b039-5e149dd4c928"),
					Name = "Samsung v32",
					Year = 2021
				},
				new FridgeModel
				{
					Id = new Guid("24964f45-61f7-4f9c-9ab8-c4981ad6a252"),
					Name = "Samsung k20",
					Year = 2019
				},
				new FridgeModel
				{
					Id = new Guid("e3130a06-9410-44c0-bc38-6145b5e60426"),
					Name = "Soyuz 1337",
					Year = 1964
				},
				new FridgeModel
				{
					Id = new Guid("b4e73b10-115e-4371-b851-9cd08cd69740"),
					Name = "Bosh c999",
					Year = 2020
				}
		};
	}
}
