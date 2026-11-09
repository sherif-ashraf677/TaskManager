using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Project, ProjectDto>()
				.ForCtorParam(nameof(ProjectDto.TaskCount), opt => opt.MapFrom(src => src.Tasks.Count))
				.ForCtorParam(nameof(ProjectDto.DoneTaskCount), opt => opt.MapFrom(src => src.Tasks.Count(t => t.Status == ProjectTaskStatus.Done)));
			CreateMap<CreateProjectDto, Project>();
			CreateMap<UpdateProjectDto, Project>();

			CreateMap<TaskItem, TaskItemDto>();
			CreateMap<CreateTaskItemDto, TaskItem>();
			CreateMap<UpdateTaskItemDto, TaskItem>();


		}
	}
}
