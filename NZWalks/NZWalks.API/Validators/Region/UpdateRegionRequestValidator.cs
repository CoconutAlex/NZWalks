﻿using FluentValidation;

namespace NZWalks.API.Validators.Region
{
    public class UpdateRegionRequestValidator : AbstractValidator<Models.DTO.Requests.Regions.UpdateRegionRequest>
    {
        public UpdateRegionRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}