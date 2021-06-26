using FilesTransfer.Config;

namespace FilesTransfer.Interfaces
{
	interface IConfigurationBuilder
	{
		Configuration Configure(string configurationPath);
	}
}
