using FluentValidation;

namespace NZWalks.API.Validators.WalkDifficulty
{
    public class AddWalkDifficultyRequestValidator : AbstractValidator<Models.DTO.Requests.WalkDifficulties.AddWalkDifficultyRequest>
    {
        public AddWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
