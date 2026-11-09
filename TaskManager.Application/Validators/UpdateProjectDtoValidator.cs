using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Validators
{
	public class UpdateProjectDtoValidator : AbstractValidator<UpdateProjectDto>
	{
		public UpdateProjectDtoValidator()
		{
			RuleFor(x => x.Name)
			.NotEmpty()
			.WithMessage("Project name is required.")
			.MaximumLength(100)
			.WithMessage("Project name must not exceed 100 characters.");
			RuleFor(x => x.Description)
				.MaximumLength(500)
				.WithMessage("Project description must not exceed 500 characters.");

		}
	}
}
