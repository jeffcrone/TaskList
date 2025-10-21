using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskList.Models;

namespace TaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[EnableCors("AllowAllOrigins")]
	public class TaskItemsController : ControllerBase
    {
        private readonly TaskContext _context;

        public TaskItemsController(TaskContext context)
        {
            _context = context;
        }

		// GET: api/TaskItems
		[HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems([FromQuery] int? startValue, [FromQuery] int? endValue)
        {
            if (startValue.HasValue && endValue.HasValue)
            {
                // Pagination
                return await _context.TaskItems
                    .Where(t => t.Id >= startValue && t.Id <= endValue)
                    .ToListAsync();
			}
			else
			{
				//No pagination
				return await _context.TaskItems.ToListAsync();
			}
        }

        // GET: api/TaskItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            return taskItem;
        }

        // PUT: api/TaskItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskItem(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaskItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItem taskItem)
        {
			if (taskItem.Id == default(int))
			{
				// Create new task item
				_context.TaskItems.Add(taskItem);
				await _context.SaveChangesAsync();
			} else
            {
				// Update existing task item
				var checkTaskItem = await _context.TaskItems.FindAsync(taskItem.Id);
				if (checkTaskItem == null)
				{
					return NotFound();
				} else
                {
                    checkTaskItem.Title = taskItem.Title;
                    checkTaskItem.Description = taskItem.Description;
                    checkTaskItem.IsComplete = taskItem.IsComplete;
					checkTaskItem.CreatedAt = taskItem.CreatedAt;
				}

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskItemExists(taskItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
			}


			return CreatedAtAction(nameof(GetTaskItem), new { id = taskItem.Id }, taskItem);
		}

        // DELETE: api/TaskItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskItemExists(int id)
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }
    }
}
