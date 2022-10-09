using FluentValidation;

namespace NZWalks.API.Validators.WalkDifficulty
{
    public class UpdateWalkDifficultyRequestValidator : AbstractValidator<Models.DTO.Requests.WalkDifficulties.UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
