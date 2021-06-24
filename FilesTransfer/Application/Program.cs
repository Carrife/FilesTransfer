using System;

namespace FilesTransfer.Application
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args[0].IsNullOrEmpty())
			{
				throw new NullReferenceException(ExceptionConstants.NullArguments);
			}

			string configurationPath = args[0];

			ApplicationRun.Run(configurationPath);
		}
	}
}
