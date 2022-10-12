using FluentValidation;

namespace NZWalks.API.Validators.Login
{
    public class LoginRequestValidator : AbstractValidator<Models.DTO.Requests.Login.LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
