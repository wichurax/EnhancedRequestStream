using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
		public async Task<ActionResult> SentMultipartData(string temp)
		{
			var json = JsonSerializer.Serialize("{\"test\": \"xD\"}");
			//var file = await System.IO.File.ReadAllBytesAsync("examplePath");

			var httpClient = new HttpClient();
			//var client = new Client("localhost:5101", httpClient);
			

			// todo ms fix gitigonre
			// todo ms - utworzyć nowego klienta
			// ogarnąć co potrzeba, żeby zbudować poprawny request
			// sprawdzić, czy swagger się do czegoś przyda

			return Ok();
		}
	}
}
