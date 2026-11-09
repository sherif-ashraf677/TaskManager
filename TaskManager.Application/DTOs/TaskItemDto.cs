using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTOs;

public record TaskItemDto(Guid Id, string Title, string? Description,
	ProjectTaskStatus Status, Priority Priority, DateTime? DueDate, Guid ProjectId);

public record CreateTaskItemDto(string Title, string? Description,
	Priority Priority, DateTime? DueDate, Guid ProjectId);

public record UpdateTaskItemDto(string Title, string? Description,
Priority Priority, DateTime? DueDate);

public record UpdateTaskStatusDto(ProjectTaskStatus Status);