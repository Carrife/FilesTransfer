using System;
using System.IO;
using System.Linq;
using FilesTransfer.Application;
using FilesTransfer.Config;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace FilesTransfer.Services
{
	class SftpService
	{
		public void Connect(Configuration configuration)
		{
			using (SftpClient client = new SftpClient(configuration.SftpHost, configuration.SftpPort, configuration.SftpUser, configuration.SftpPassword))
			{
				Console.WriteLine("SFTP connection..");

				client.Connect();

				if (!client.IsConnected)
				{
					throw new Exception(ExceptionConstants.IncorrectSettings);
				}

				ReceiveData(configuration, client);

				client.Disconnect();

				Console.WriteLine("Сlient disconnected.\n");
			}
		}

		private void ReceiveData(Configuration configuration, SftpClient client)
		{
			Console.WriteLine("Receiving data...");

			if (!client.Exists(configuration.SftpRemoteDir))
			{
				throw new Exception(ExceptionConstants.IncorrectPath);
			}
			
			client.ChangeDirectory(configuration.SftpRemoteDir);

			DownloadFile(configuration, client);
		}

		private void DownloadFile(Configuration configuration, SftpClient client)
		{
			SftpFile[] eachRemoteFile = client.ListDirectory(configuration.SftpRemoteDir).ToArray();
			foreach (SftpFile remoteFile in eachRemoteFile)
			{
				if (remoteFile.IsRegularFile)
				{
					string eachFileNameInArchive = remoteFile.Name;

					using (var file = File.OpenWrite(configuration.LocalDir + eachFileNameInArchive))
					{
						client.DownloadFile(eachFileNameInArchive, file);
					}
				}
			}
		}
	}
}
