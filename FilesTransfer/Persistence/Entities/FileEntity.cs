using System;

namespace FilesTransfer.Persistence.Entities
{
	public class FileEntity
	{
		public int Id { get; set; }
		public string Filename { get; set; }
		public DateTime Copytime { get; set; }
	}
}
