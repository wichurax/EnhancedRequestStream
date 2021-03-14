using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Server2.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class Server2Controller : ControllerBase
	{
		private readonly ILogger<Server2Controller> _logger;

		public Server2Controller(ILogger<Server2Controller> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		[DisableRequestSizeLimit]
		public async Task<IActionResult> SentFileWithConfiguration([FromForm] FileWithConfigurationForm form)
		{
			_logger.LogInformation($"Download of FileWithConfigurationForm in server2 started at: {DateTimeOffset.Now:o}");
			_logger.LogInformation($"Download of file '{form.File.Name}' in server2 started at: {DateTimeOffset.Now:o}");

			using (StreamReader sr = new StreamReader(form.File.OpenReadStream()))
			{
				var streamValue = await sr.ReadToEndAsync();
				_logger.LogInformation($"Download of file '{form.File.Name}' in server2 finished at: {DateTimeOffset.Now:o}");
				//_logger.LogInformation($"Stream value: {streamValue}");
			}

			_logger.LogInformation($"Download of FileWithConfigurationForm in server2 finished at: {DateTimeOffset.Now:o}");

			return Ok();
		}

		public class FileWithConfigurationForm
		{
			[FromForm(Name = "file")]
			public IFormFile File { get; set; }

			[FromForm(Name = "configuration")]
			public string Configuration { get; set; }
		}
	}
}
