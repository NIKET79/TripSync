namespace Application.MediatR.Authorization.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotNull()
            .EmailAddress();

        RuleFor(command => command.Password)
            .NotNull();
    }
}