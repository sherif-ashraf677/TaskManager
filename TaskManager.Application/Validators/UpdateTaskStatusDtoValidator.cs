using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Validators
{
	public class UpdateTaskStatusDtoValidator:AbstractValidator<UpdateTaskStatusDto>
	{
		public UpdateTaskStatusDtoValidator()
		{
			RuleFor(x => x.Status)
			.IsInEnum()
			.WithMessage("Invalid Status");
		}
	}
}
