
namespace LogsToPageProject.Domain
{
	using System.Collections.Generic;

	public class MicroServiceLogs
	{
		public MicroServiceLogs()
		{
			this.Logs=new List<Log>();
		}
		public string MicroServiceName { get; set; }

		public ICollection<Log> Logs { get; set; }

	}
}
