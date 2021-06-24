using System;
using System.IO;
using FilesTransfer.Config;
using FilesTransfer.Services;

namespace FilesTransfer.Application
{
	class ApplicationRun
	{
        public static void Run(string configurationPath)
        {
            if (!File.Exists(configurationPath))
            {
                throw new Exception(ExceptionConstants.IncorrectPath);
            }

            var configuration = new ConfigurationBuilder().ConfigureFromCustom(configurationPath);
            //var configuration = new ConfigurationBuilder().ConfigureFromJSON(configurationPath);

            new SftpService().Connect(configuration);

            //new DataBaseService().Entry(configuration);
            new DataBaseService().EntryUsingEntityFramework(configuration);
        }
    }
}
