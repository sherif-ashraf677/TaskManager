using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Validators
{
	public class UpdateTaskItemDtoValidator : AbstractValidator<UpdateTaskItemDto>
	{
		public UpdateTaskItemDtoValidator()
		{
			RuleFor(x=>x.Title)
				.NotEmpty().WithMessage("Title is required.")
				.MaximumLength(200).WithMessage("Title must not exceed 100 characters.");
			RuleFor(x => x.Description)
				.MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
			RuleFor(x => x.Priority)
			.IsInEnum()
			.WithMessage("Invalid priority value");
		}
	}
}
