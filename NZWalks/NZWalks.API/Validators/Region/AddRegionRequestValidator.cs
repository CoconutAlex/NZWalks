using FluentValidation;

namespace NZWalks.API.Validators.Region
{
    public class AddRegionRequestValidator : AbstractValidator<Models.DTO.Requests.Regions.AddRegionRequest>
    {
        public AddRegionRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
