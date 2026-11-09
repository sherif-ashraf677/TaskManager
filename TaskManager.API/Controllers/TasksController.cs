using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfacies;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class TasksController : ApiControllerBase
	{
		private readonly ITaskRepository _taskRepository;

		private readonly IProjectRepository _projectRepository;
		private readonly IMapper _mapper;

		public TasksController(ITaskRepository taskRepository, IProjectRepository projectRepository, IMapper mapper)
		{
			_taskRepository = taskRepository;
			_projectRepository = projectRepository;
			_mapper = mapper;
		}

		[HttpGet("{projectId}")]
		public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAllByProjectAsync(Guid projectId)
		{
			var project = await _projectRepository.GetByIdAsync(projectId, CurrentUserId);
			if (project == null)
			{
				return NotFound();
			}
			var tasks = await _taskRepository.GetAllByProjectAsync(projectId);
			var taskDtos = _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
			return Ok(taskDtos);
		}

		[HttpGet("{projectId}/{taskId}")]

		public async Task<ActionResult<TaskItemDto>> GetByIdAsync(Guid projectId, Guid taskId)
		{
			var project = await _projectRepository.GetByIdAsync(projectId, CurrentUserId);
			if (project is null) return NotFound();
			var task = await _taskRepository.GetByIdAsync(taskId, projectId);
			if (task == null)
			{
				return NotFound();
			}
			var taskDto = _mapper.Map<TaskItemDto>(task);
			return Ok(taskDto);

		}

		[HttpPost]

		public async Task<ActionResult<TaskItemDto>> CreateTaskAsync(CreateTaskItemDto createTaskDto, Guid projectId)
		{
			var project = await _projectRepository.GetByIdAsync(projectId, CurrentUserId);
			if (project is null) return NotFound();

			var task = _mapper.Map<TaskItem>(createTaskDto);
			task.ProjectId = projectId;
			task.Status = ProjectTaskStatus.Todo;
			await _taskRepository.AddAsync(task);
			await _taskRepository.SaveChangesAsync();
			var taskDto = _mapper.Map<TaskItemDto>(task);
			return Created($"/api/Tasks/{task.ProjectId}/{task.Id}", taskDto);
		}

		[HttpPut("{projectId}/{taskId}")]

		public async Task<ActionResult> UpdateTaskAsync(Guid projectId, Guid taskId, UpdateTaskItemDto updateTaskDto)
		{
			var project = await _projectRepository.GetByIdAsync(projectId, CurrentUserId);
			if (project is null) return NotFound();
			var task = await _taskRepository.GetByIdAsync(taskId, projectId);
			if (task == null)
			{
				return NotFound();
			}
			task.Title = updateTaskDto.Title;
			task.Description = updateTaskDto.Description;
			task.Priority = updateTaskDto.Priority;
			task.DueDate = updateTaskDto.DueDate;
			 _taskRepository.Update(task);
			await _taskRepository.SaveChangesAsync();
			return NoContent();


		}

		[HttpPatch("{projectId}/{taskId}/status")]
		public async Task<ActionResult> UpdateTaskStatusAsync(Guid projectId, Guid taskId,  UpdateTaskStatusDto dto)
		{
			var project = await _projectRepository.GetByIdAsync(projectId, CurrentUserId);
			if (project is null) return NotFound();

			var task = await _taskRepository.GetByIdAsync(taskId, projectId);
			if (task == null)
			{
				return NotFound();
			}
			task.Status = dto.Status;
			_taskRepository.Update(task);
			await _taskRepository.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{projectId}/{taskId}")]
		public async Task<ActionResult> DeleteTaskAsync(Guid projectId, Guid taskId)
		{
			var project = await _projectRepository.GetByIdAsync(projectId, CurrentUserId);
			if (project is null) return NotFound();
			var task = await _taskRepository.GetByIdAsync(taskId, projectId);
			if (task == null)
			{
				return NotFound();
			}
			_taskRepository.Delete(task);
			await _taskRepository.SaveChangesAsync();
			return NoContent();
		}
		
	}
}
