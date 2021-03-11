using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Server1.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[HttpPost("/sent-multipart-data")]
		public async Task<IActionResult> SentFileWithConfiguration([FromForm] FileWithConfigurationForm form)
		{
			// todo ms
			// var file = await System.IO.File.ReadAllBytesAsync("C:/EnhancedRequestStream/EnhancedRequestStreamFile.txt");
			
			if (!Request.ContentType.Contains("multipart/mixed"))
			{
				return new UnsupportedMediaTypeResult();
			}

			return Ok();
		}
	}

	public class FileWithConfigurationForm
	{
		[FromForm(Name = "file")]
		public IFormFile File { get; set; }

		[FromForm(Name = "configuration")]
		public string Configuration { get; set; }
	}
}
