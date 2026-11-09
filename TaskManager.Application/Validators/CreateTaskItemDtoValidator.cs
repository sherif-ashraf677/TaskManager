using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Validators
{
	public class CreateTaskItemDtoValidator : AbstractValidator<CreateTaskItemDto>
	{
		public CreateTaskItemDtoValidator()
		{
			RuleFor(x=>x.Title)
				.NotEmpty()
				.WithMessage("Task title is required.")
				.MaximumLength(200)
				.WithMessage("Task title must not exceed 200 characters.");
				RuleFor(x => x.Description)
				.MaximumLength(1000)
				.WithMessage("Task description must not exceed 1000 characters.");
				RuleFor(x=>x.Priority)
				.IsInEnum()
				.WithMessage("Invalid priority value.");
				RuleFor(x => x.DueDate)
				.GreaterThanOrEqualTo(DateTime.UtcNow.Date)
				.When(x => x.DueDate.HasValue)
				.WithMessage("the date is old");
				
		}
	}
}
