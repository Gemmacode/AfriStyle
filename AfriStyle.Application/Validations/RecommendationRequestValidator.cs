using AfriStyle.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Application.Validations
{
    public class RecommendationRequestValidator : AbstractValidator<RecommendationRequestDto>
    {
        public RecommendationRequestValidator()
        {
            RuleFor(x => x.ForeheadWidth)
                .GreaterThan(0).LessThan(5)
                .WithMessage("ForeheadWidth must be a valid ratio (0–5).");

            RuleFor(x => x.CheekboneWidth)
                .GreaterThan(0).LessThan(5)
                .WithMessage("CheekboneWidth must be a valid ratio (0–5).");

            RuleFor(x => x.JawWidth)
                .GreaterThan(0).LessThan(5)
                .WithMessage("JawWidth must be a valid ratio (0–5).");

            RuleFor(x => x.FaceLength)
                .GreaterThan(0).LessThan(5)
                .WithMessage("FaceLength must be a valid ratio (0–5).");

            RuleFor(x => x.JawAngle)
                .InclusiveBetween(0, 1)
                .WithMessage("JawAngle must be between 0 and 1.");

            RuleFor(x => x.HairTexture)
                .IsInEnum()
                .WithMessage("Invalid hair texture selection.");

            RuleFor(x => x.Occasion)
                .IsInEnum()
                .WithMessage("Invalid occasion selection.");
        }
    }
}
