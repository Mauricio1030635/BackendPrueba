using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SittyCia.Core.Dto;
using SittyCia.Core.Models;
using SittyCia.Core.Repository;


namespace SittyCia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTasksByUserId(string userId)
        {
            var tasks = await _taskService.GetTasksByUserIdAsync(userId);
            var taskDtos = _mapper.Map<IEnumerable<TaskDto>>(tasks);
            return Ok(taskDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null) return NotFound();

            var taskDto = _mapper.Map<TaskDto>(task);
            return Ok(taskDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            var task = _mapper.Map<TaskEntity>(createTaskDto);
            var newTask = await _taskService.CreateAsync(task);
            var taskDto = _mapper.Map<TaskDto>(newTask);
            return CreatedAtAction(nameof(GetTaskById), new { id = taskDto.Id }, taskDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskDto updateTaskDto)
        {
            var task = _mapper.Map<TaskEntity>(updateTaskDto);
            var updatedTask = await _taskService.UpdateAsync(task);
            var taskDto = _mapper.Map<TaskDto>(updatedTask);
            return Ok(taskDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }

}
