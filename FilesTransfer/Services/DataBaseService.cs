using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FilesTransfer.Application;
using FilesTransfer.Config;
using FilesTransfer.Persistence;
using FilesTransfer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace FilesTransfer.Services
{
	class DataBaseService
	{
		public void Entry(Configuration configuration)
		{
			string connection = new Connection().GetConnectionString(configuration);

			using (MySqlConnection connect = new MySqlConnection(connection))
			{
				Console.WriteLine("DB connection..\n");

				connect.Open();

				AddData(configuration, connect);
				ReceiveData(connect);

				connect.Close();

				Console.WriteLine("\nDB disconnected.");
			}
		}

		private void AddData(Configuration configuration, MySqlConnection connect)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(configuration.LocalDir);
			if (!directoryInfo.Exists)
			{
				throw new Exception(ExceptionConstants.IncorrectPath);
			}

			FileInfo[] fileInfo = directoryInfo.GetFiles();

			foreach (FileInfo file in fileInfo)
			{
				string query = "INSERT INTO files (filename, copytime) VALUES (@file, @time)";

				MySqlCommand add = new MySqlCommand(query, connect);

				add.Parameters.AddWithValue("@file", file.Name);
				add.Parameters.AddWithValue("@time", file.CreationTime);

				add.ExecuteNonQuery();
			}
		}

		private void ReceiveData(MySqlConnection connect)
		{
			string query = "SELECT filename, copytime FROM files";

			MySqlCommand receive = new MySqlCommand(query, connect);

			MySqlDataReader reader = receive.ExecuteReader();
			while (reader.Read())
			{
				Console.WriteLine(reader[0].ToString().PadRight(40) + reader[1].ToString());
			}
			reader.Close();
		}


		public void EntryUsingEntityFramework(Configuration configuration)
		{
			Console.WriteLine("DB connection..\n");

			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDataContext>();
			string connection = new Connection().GetConnectionString(configuration);

			var options = optionsBuilder
					.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 11)))
					.Options;

			using (ApplicationDataContext context = new ApplicationDataContext(options))
			{
				AddDataUsingEntityFramework(configuration, context);
				ReceiveDataUsingEntityFramework(context);
			}

			Console.WriteLine("\nDB disconnected.");
		}

		private void AddDataUsingEntityFramework(Configuration configuration, ApplicationDataContext context)
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

		private void ReceiveDataUsingEntityFramework(ApplicationDataContext context)
		{
			var information = context.files.ToList();
			foreach (var item in information)
			{
				Console.WriteLine(item.Filename.ToString().PadRight(40) + item.Copytime.ToString());
			}
		}
	}
}
