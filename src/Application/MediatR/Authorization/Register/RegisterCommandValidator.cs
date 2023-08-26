namespace Application.MediatR.Authorization.Register;

public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotNull()
            .EmailAddress();

        RuleFor(command => command.Username)
            .NotNull()
            .MinimumLength(3);

        RuleFor(command => command.ConfirmPassword)
            .NotNull()
            .Equal(command => command.Password);
    }
}