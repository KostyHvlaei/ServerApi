using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ServerApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[ApiExplorerSettings(GroupName = "v1")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILoggerManager _logger;
		private readonly IRepositoryManager _repository;

		public WeatherForecastController(ILoggerManager logger, IRepositoryManager repository)
		{
			_logger = logger;
			_repository = repository;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var res = _repository.Fridge.GetAllFridges(true);

			return Ok(res);
		}
	}
}
