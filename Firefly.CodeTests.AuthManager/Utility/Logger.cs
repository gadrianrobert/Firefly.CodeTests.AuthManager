using System;
using System.Threading;

namespace Firefly.CodeTests.AuthManager.Utility
{
	public class Logger
	{
		private static readonly Lazy<Logger> logger = new Lazy<Logger>(() =>  new Logger(), LazyThreadSafetyMode.ExecutionAndPublication);
		private Logger()
		{
			
		}

		public static Logger Instance => logger.Value;

		public void LogException(Exception ex)
		{
			//TODO: log the actual exception
		}
	}
}
