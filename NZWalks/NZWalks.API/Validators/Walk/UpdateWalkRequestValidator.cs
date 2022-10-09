using FluentValidation;

namespace NZWalks.API.Validators.Walk
{
    public class UpdateWalkDifficultyRequestValidator : AbstractValidator<Models.DTO.Requests.Walks.UpdateWalkRequest>
    {
        public UpdateWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
            RuleFor(x => x.RegionId).NotEmpty();
            RuleFor(x => x.WalkDifficultyId).NotEmpty();
        }
    }
}
