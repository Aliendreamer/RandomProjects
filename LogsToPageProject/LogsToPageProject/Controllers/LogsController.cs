﻿namespace LogsToPageProject.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Services;

	[ApiController]
	[Route("api/logs")]
	public class LogsController:ControllerBase
	{
		private readonly ILogService _logService;

		public LogsController(ILogService logService)
		{
			_logService = logService;
		}

		[HttpGet]
		public IActionResult GetLogs()
		{
			var logs=_logService.GetLogsPerServices();

			return Ok(logs);
		}

		[HttpGet]
		[Route("{serviceName}")]
		public IActionResult GetServiceLogs(string serviceName)
		{
			var serviceLogs = this._logService.GetLogsForService(serviceName);

			return Ok(serviceLogs);
		}

		[HttpDelete]
		[Route("{serviceName}/{id}")]
		public IActionResult DeleteLog(string serviceName,int id)
		{
			var logsLeft= this._logService.DeleteLogByIndex(serviceName, id);
			return Ok(logsLeft);
		}
	}
}
