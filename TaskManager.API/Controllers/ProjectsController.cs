using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfacies;
using TaskManager.Domain.Entities;
namespace TaskManager.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ProjectsController : ApiControllerBase
	{
		private readonly IProjectRepository _projectRepository;
		private readonly IMapper _mapper;

		
		public ProjectsController(IProjectRepository projectRepository, IMapper mapper)
		{
			_projectRepository = projectRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult> GetAllProjects()
		{
			var projects = await _projectRepository.GetAllByOwnerAsync(CurrentUserId);
			var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
			return Ok(projectDtos);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetById(Guid id) {

			var project = await _projectRepository.GetByIdAsync(id, CurrentUserId);
			if (project == null)
			{
				return NotFound();
			}
			var projectDto = _mapper.Map<ProjectDto>(project);
			return Ok(projectDto);
		}

		[HttpPost]
		public async Task<ActionResult> CreateProject(CreateProjectDto projectDto)
		{
		
			var project = _mapper.Map<Project>(projectDto);
			project.OwnerId = CurrentUserId;
			await _projectRepository.AddAsync(project);
			await _projectRepository.SaveChangesAsync();

			var resultDto = _mapper.Map<ProjectDto>(project);
			return CreatedAtAction(nameof(GetById), new { id = project.Id}, resultDto);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateProject(Guid id, UpdateProjectDto projectDto){
			var project = await _projectRepository.GetByIdAsync(id, CurrentUserId);
			if (project == null)
			{
				return NotFound();
			}
			project.Name = projectDto.Name;
			project.Description = projectDto.Description;
			_projectRepository.Update(project);
			await _projectRepository.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteProject(Guid id)
		{ 
			var project = await _projectRepository.GetByIdAsync(id, CurrentUserId);
			if (project == null)
			{
				return NotFound();
			}
			_projectRepository.Delete(project);
			await _projectRepository.SaveChangesAsync();
			return NoContent();

		}

	}
}
