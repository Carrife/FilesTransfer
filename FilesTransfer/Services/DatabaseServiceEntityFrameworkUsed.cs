using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FilesTransfer.Application;
using FilesTransfer.Config;
using FilesTransfer.Persistence;
using FilesTransfer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilesTransfer.Services
{
	class DatabaseServiceEntityFrameworkUsed
	{
		public void Entry(Configuration configuration)
		{
			Console.WriteLine("DB connection..\n");

			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDataContext>();
			string connection = new Connection().GetConnectionString(configuration);

			var options = optionsBuilder
					.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 11)))
					.Options;

			using (ApplicationDataContext context = new ApplicationDataContext(options))
			{
				AddData(configuration, context);
				ReceiveData(context);
			}

			Console.WriteLine("\nDB disconnected.");
		}

		private void AddData(Configuration configuration, ApplicationDataContext context)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(configuration.LocalDir);
			if (!directoryInfo.Exists)
			{
				throw new Exception(ExceptionConstants.IncorrectPath);
			}

			FileInfo[] fileInfo = directoryInfo.GetFiles();

			var files = new List<FileEntity>();

			foreach (FileInfo file in fileInfo)
			{
				var data = new FileEntity()
				{
					Filename = file.Name,
					Copytime = file.CreationTime
				};

				files.Add(data);
			}

			context.files.AddRange(files);
			context.SaveChanges();
		}

		private void ReceiveData(ApplicationDataContext context)
		{
			var information = context.files.ToList();
			foreach (var item in information)
			{
				Console.WriteLine(item.Filename.ToString().PadRight(40) + item.Copytime.ToString());
			}
		}
	}
}
