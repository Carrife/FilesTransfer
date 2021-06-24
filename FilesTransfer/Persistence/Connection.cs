using FilesTransfer.Config;

namespace FilesTransfer.Persistence
{
	class Connection
	{
		public string GetConnectionString (Configuration configuration)
		{
			return $"server=localhost;database={configuration.SqlDatabase};uid={configuration.SqlUser};pwd={configuration.SqlPassword};";
		}
	}
}
