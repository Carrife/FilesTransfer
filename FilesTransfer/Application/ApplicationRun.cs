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

            //var configuration = new JsonConfigurationBuilder().Configure(configurationPath);
            var configuration = new CustomConfigurationBuilder().Configure(configurationPath);

            new SftpService().Connect(configuration);

            //new DatabaseService().Entry(configuration);
            new DatabaseServiceEntityFrameworkUsed().Entry(configuration);
        }
    }
}
