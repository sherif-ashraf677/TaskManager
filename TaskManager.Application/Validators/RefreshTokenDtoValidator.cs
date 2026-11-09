using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Validators
{
	public class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenDto> 
	{
		public RefreshTokenDtoValidator()
		{
			RuleFor(x => x.RefreshToken)
			.NotEmpty()
			.WithMessage("this field is required");
		}
	}
}
