namespace TaskList.Models
{
	public class TaskItem
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public required string Description { get; set; }
		public bool IsComplete { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
