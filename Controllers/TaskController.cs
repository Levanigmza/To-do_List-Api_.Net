using Microsoft.AspNetCore.Mvc;
using To_Do_List_Web_Api.Data;
using Microsoft.EntityFrameworkCore;
using To_Do_List_Web_Api.Models.To_Do_List_Web_Api.Models;

namespace To_Do_List_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
        {
            var taskItems = await _context.TaskItems
                .ToListAsync();

            var mappedTaskItems = taskItems.Select(ti => new
            {
                ti.ID,
                ti.TaskName,
                TaskStatusID = ti.TaskStatusID,
                StatusName = MapStatusName(ti.TaskStatusID)
            });

            return Ok(mappedTaskItems);
        }

        private string MapStatusName(int statusID)
        {
            return statusID == 1 ? "მიმდინარე" : "დასრულებული";
        }


        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItem taskItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskItems), new { id = taskItem.ID }, taskItem);
        }


        [HttpPut("update/{taskId}")]
        public IActionResult UpdateTask(int taskId, [FromBody] TaskUpdate updatedTask)
        {
            if (taskId != updatedTask.ID)
            {
                return BadRequest();
            }

            var existingTask = _context.TaskItems.FirstOrDefault(t => t.ID == taskId);

            if (existingTask == null)
            {
                return NotFound("დავალება არ მოიძებნა");
            }

            existingTask.TaskName = updatedTask.TaskName;
            existingTask.TaskStatusID = updatedTask.TaskStatusID;

            _context.SaveChanges();

            return Ok("დავალება დარედაქტირდა წარამატებით");
        }

        [HttpDelete("delete/{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            var taskToDelete = _context.TaskItems.FirstOrDefault(t => t.ID == taskId);

            if (taskToDelete == null)
            {
                return NotFound("დავალება არ მოიძებნა");
            }

            _context.TaskItems.Remove(taskToDelete);
            _context.SaveChanges();

            return Ok("დავალება წაიშალა წარამატებით");
        }


    }
}
