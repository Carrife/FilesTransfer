using System.IO;
using FilesTransfer.Interfaces;
using Newtonsoft.Json;

namespace FilesTransfer.Config
{
	class JsonConfigurationBuilder : IConfigurationBuilder
	{
		public Configuration Configure(string configurationPath)
		{
			string configurationContent = File.ReadAllText(configurationPath);

			var configuration = JsonConvert.DeserializeObject<Configuration>(configurationContent);

			return configuration;
		}
	}
}
