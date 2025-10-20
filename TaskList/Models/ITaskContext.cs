using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TaskList.Models
{
	public interface ITaskContext : IDisposable, IAsyncDisposable
	{
		string DbPath { get; }

		DbSet<TaskItem> TaskItems { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

		int SaveChanges();
	}
}