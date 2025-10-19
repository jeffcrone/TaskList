using Microsoft.EntityFrameworkCore;

namespace TaskList.Models
{
	public class TaskContext : DbContext
	{
		public TaskContext(DbContextOptions<TaskContext> options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=tasklist.db");
		}

		public DbSet<TaskItem> TaskItems { get; set; } = null!;
	}
}
