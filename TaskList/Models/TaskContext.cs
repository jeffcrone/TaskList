using System;
using Microsoft.EntityFrameworkCore;

namespace TaskList.Models
{
	public class TaskContext : DbContext, ITaskContext
	{
		public string DbPath { get; }

		public TaskContext(DbContextOptions<TaskContext> options) : base(options)
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			DbPath = System.IO.Path.Join(path, "tasklist.db");
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=tasklist.db");
		}

		public DbSet<TaskItem> TaskItems { get; set; } = null!;
	}
}
