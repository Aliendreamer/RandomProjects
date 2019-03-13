namespace LogsToPageProject.Domain
{
	using System;

	public class Log
	{
		public DateTime LastModified { get; set; }

		public string Name { get; set; }

		public string Message { get; set; }
	}
}
