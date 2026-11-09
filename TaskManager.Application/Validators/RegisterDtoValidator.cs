using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Validators
{
	public class RegisterDtoValidator : AbstractValidator<RegisterDto>
	{
		public RegisterDtoValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email is required.")
				.EmailAddress().WithMessage("Invalid email format.");
			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Password is required.")
				.MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
				.Matches(@"[A-Z]")
				.WithMessage("Password must contain at least one uppercase letter.")
				.Matches(@"[0-9]")
				.WithMessage("Password must contain at least one number.")
				.Matches(@"[a-z]")
				.WithMessage("Password must contain at least one lowerCase letter.");
			RuleFor(x => x.ConfirmPassword)
			.NotEmpty().WithMessage("Confirm Password is required.")
				.Equal(x => x.Password).WithMessage("Passwords do not match.");
		}
	}
}
