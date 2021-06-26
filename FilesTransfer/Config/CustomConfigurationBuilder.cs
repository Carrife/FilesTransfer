using System.IO;
using FilesTransfer.Interfaces;

namespace FilesTransfer.Config
{
	class CustomConfigurationBuilder : IConfigurationBuilder
	{
		public Configuration Configure(string configurationPath)
		{
			string[] sub;
			var configuration = new Configuration();
			foreach (var line in File.ReadLines(configurationPath))
			{
				sub = line.Split('=');

				switch (sub[0])
				{
					case "sftp_host":
						configuration.SftpHost = sub[1];
						break;
					case "sftp_port":
						configuration.SftpPort = int.Parse(sub[1]);
						break;
					case "sftp_user":
						configuration.SftpUser = sub[1];
						break;
					case "sftp_password":
						configuration.SftpPassword = sub[1];
						break;
					case "sftp_remote_dir":
						configuration.SftpRemoteDir = sub[1];
						break;
					case "local_dir":
						configuration.LocalDir = sub[1];
						break;
					case "sql_user":
						configuration.SqlUser = sub[1];
						break;
					case "sql_password":
						configuration.SqlPassword = sub[1];
						break;
					case "sql_database":
						configuration.SqlDatabase = sub[1];
						break;
				}
			}
			return configuration;
		}
	}
}
