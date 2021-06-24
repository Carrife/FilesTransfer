using FilesTransfer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilesTransfer.Persistence
{
	public class ApplicationDataContext : DbContext
	{
		public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) {}

		public DbSet<FileEntity> files { get; set; }
	}
}
