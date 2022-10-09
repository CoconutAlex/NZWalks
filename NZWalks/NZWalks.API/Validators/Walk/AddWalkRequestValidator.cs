using FluentValidation;

namespace NZWalks.API.Validators.Walk
{
    public class AddWalkRequestValidator : AbstractValidator<Models.DTO.Requests.Walks.AddWalkRequest>
    {
        public AddWalkRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
