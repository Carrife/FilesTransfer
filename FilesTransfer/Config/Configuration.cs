using Newtonsoft.Json;

namespace FilesTransfer.Config
{
	class Configuration
	{
		[JsonProperty("sftp_host")]
		public string SftpHost { get; set; }

		[JsonProperty("sftp_port")]
		public int SftpPort { get; set; }
		
		[JsonProperty("sftp_user")]
		public string SftpUser { get; set; }
		
		[JsonProperty("sftp_password")]
		public string SftpPassword { get; set; }
		
		[JsonProperty("sftp_remote_dir")]
		public string SftpRemoteDir { get; set; }
		
		[JsonProperty("local_dir")]
		public string LocalDir { get; set; }
		
		[JsonProperty("sql_user")]
		public string SqlUser { get; set; }
		
		[JsonProperty("sql_password")]
		public string SqlPassword { get; set; }
		
		[JsonProperty("sql_database")]
		public string SqlDatabase { get; set; }
	}
}
