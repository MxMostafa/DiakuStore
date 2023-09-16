

namespace DiakuSoft.Server.API.Applications.Validators.Auth;
public class LoginReqDtoValidator : AbstractValidator<LoginReqDto>
{
    public LoginReqDtoValidator()
    {
        RuleFor(x => x.UserName)
            .Matches(RegexPattern.EmailPattern)
            .WithMessage(ValidationErrorMessages.EmailPatternError);

        RuleFor(x => x.Password)
             .Matches(RegexPattern.PasswordPattern)
             .WithMessage(ValidationErrorMessages.PasswordPatternError);
    }
}
