using System;
using System.IO;
using FilesTransfer.Application;
using FilesTransfer.Config;
using FilesTransfer.Persistence;
using MySqlConnector;

namespace FilesTransfer.Services
{
	class DatabaseService
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
	}
}
