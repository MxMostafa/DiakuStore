namespace DiakuSoft.Server.API.Applications.Validators.Auth;

public class RegistrationWithEmailValidator:AbstractValidator<RegisterationByEmailReqDto>
{
    public RegistrationWithEmailValidator()
    {
        RuleFor(x => x.Email)
            .Matches(RegexPattern.EmailPattern)
            .WithMessage(ValidationErrorMessages.EmailPatternError);

        RuleFor(x => x.Password)
            .Matches(RegexPattern.PasswordPattern)
            .WithMessage(ValidationErrorMessages.PasswordPatternError);

        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage(ValidationErrorMessages.FirstNameValidationError);

        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage(ValidationErrorMessages.LastNameValidationError);

    }
}
