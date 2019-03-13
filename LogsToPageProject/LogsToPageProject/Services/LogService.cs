namespace LogsToPageProject.Services
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Domain;
	using Microsoft.Extensions.FileProviders;


	public interface ILogService
	{
		IEnumerable<MicroServiceLogs> GetLogsPerServices();
		MicroServiceLogs GetLogsForService(string subPath);
		MicroServiceLogs DeleteLogByIndex(string dir, int index);
	}

	public class LogService:ILogService
	{
		private readonly IFileProvider _fileProvider;

		public LogService(IFileProvider provider)
		{
			this._fileProvider = provider;
		}

		private readonly string mainDirectoryPath = Constants.DirectoryPath;

		public MicroServiceLogs DeleteLogByIndex(string dir,int index)
		{
			var serviceLogs = GetLogs(dir);
			serviceLogs.Logs.ToList().RemoveAt(index);
			return serviceLogs;
		}

		public MicroServiceLogs GetLogsForService(string subPath)
		{
			var serviceLogs = GetLogs(subPath);
			return serviceLogs;
		}

		public IEnumerable<MicroServiceLogs> GetLogsPerServices()
		{
			var directories = Directory.GetDirectories(mainDirectoryPath);

			ICollection<MicroServiceLogs>serviceLogs=new List<MicroServiceLogs>();
			foreach (var dir in directories)
			{
				string element = dir.Split("\\", StringSplitOptions.RemoveEmptyEntries).ToArray().Last();
				var currentMicroService = GetLogs(element);
				serviceLogs.Add(currentMicroService);
			}

			return serviceLogs;
		}

		private MicroServiceLogs GetLogs(string dir)
		{
			var service = new MicroServiceLogs {MicroServiceName = dir};
			var files = this._fileProvider.GetDirectoryContents(dir);
			foreach (var f in files)
			{
			 var log=new Log();
			 string content=File.ReadAllText(f.PhysicalPath);
			 log.Message = content;
			 log.Name = f.Name;
			 log.LastModified = f.LastModified.DateTime;
			 service.Logs.Add(log);
			}
			return service;
		} 
	}
}
