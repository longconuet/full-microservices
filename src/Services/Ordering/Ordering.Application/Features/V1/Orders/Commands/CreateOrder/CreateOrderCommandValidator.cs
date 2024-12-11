﻿using FluentValidation;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("{UserName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters");

            RuleFor(x => x.EmailAddress)
                .EmailAddress().WithMessage("{EmailAddress} is invalid email")
                .NotEmpty().WithMessage("{EmailAddress} is required");

            RuleFor(x => x.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is required")
                .GreaterThan(0).WithMessage("{TotalPrice} should be greater than 0");
        }
    }
}
