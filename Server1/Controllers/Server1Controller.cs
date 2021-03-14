using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server2ApiClient;

namespace Server1.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class Server1Controller : ControllerBase
	{
		private readonly ILogger<Server1Controller> _logger;
		private readonly HttpClient _httpClient = new HttpClient();

		public Server1Controller(ILogger<Server1Controller> logger)
		{
			_logger = logger;
		}

		[HttpPost("/sent-file")]
		[DisableRequestSizeLimit]
		public async Task<IActionResult> SentFileWithConfiguration(IFormFile file)
		{
			_logger.LogInformation($"Download of file '{file.Name}' in server1 started at: {DateTimeOffset.Now:o}");

			var server2Client = new Server2Client("https://localhost:5201/", _httpClient);

			var fileParam = new FileParameter(file.OpenReadStream());
			await server2Client.Server2Async(fileParam, "{\"test\": \"xD\"");

			_logger.LogInformation($"Download of file '{file.Name}' in server1 finished at: {DateTimeOffset.Now:o}");

			return Ok();
		}
	}
}
