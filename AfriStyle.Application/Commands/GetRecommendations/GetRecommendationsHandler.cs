using AfriStyle.Application.DTOs;
using AfriStyle.Application.Interfaces;
using AfriStyle.Domain.Enums;
using AfriStyle.Domain.ValueObjects;
using MediatR;
using Mapster;
using Microsoft.Extensions.Logging;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AfriStyle.Domain.Services;

namespace AfriStyle.Application.Commands.GetRecommendations
{
    public class GetRecommendationsHandler
    : IRequestHandler<GetRecommendationsCommand, RecommendationResponseDto>
    {
        private readonly IStyleRepository _styleRepo;
        private readonly FaceShapeClassifier _classifier;       
        private readonly StyleScoringEngine _scorer;
        private readonly ILogger<GetRecommendationsHandler> _logger;

        public GetRecommendationsHandler(
            IStyleRepository styleRepo,
            FaceShapeClassifier classifier,
            StyleScoringEngine scorer,
            ILogger<GetRecommendationsHandler> logger)
        {
            _styleRepo = styleRepo;
            _classifier = classifier;
            _scorer = scorer;
            _logger = logger;
        }

        public async Task<RecommendationResponseDto> Handle(
            GetRecommendationsCommand command,
            CancellationToken cancellationToken)
        {
            var req = command.Request;
           
            var measurements = new FaceMeasurements(
                foreheadWidth: req.ForeheadWidth,
                cheekboneWidth: req.CheekboneWidth,
                jawWidth: req.JawWidth,
                faceLength: req.FaceLength,
                jawAngle: req.JawAngle
            );
            
            (FaceShape shape, double confidence) = _classifier.Classify(measurements);
            _logger.LogInformation("Face classified as {Shape} ({Confidence:P0})", shape, confidence);
           
            var allStyles = await _styleRepo.GetAllAsync(req.ForMen, cancellationToken);

            var scored = allStyles
                .Select(style => new
                {
                    Style = style,
                    Score = _scorer.Score(style, shape, req.HairTexture, req.Occasion)
                })
                .OrderByDescending(x => x.Score.Total)
                .Take(8)
                .ToList();
            
            var recommendations = scored
                .Select(x => (x.Style, x.Score).Adapt<StyleResultDto>())
                .ToList();
          
            return new RecommendationResponseDto(
                AnalysisId: Guid.NewGuid().ToString(),
                DetectedFaceShape: shape,
                FaceShapeName: shape.ToString(),
                Confidence: confidence,
                FaceShapeDescription: GetDescription(shape),
                FaceShapeTip: GetAfriStyleTip(shape),
                Recommendations: recommendations
            );
        }

        // ── AfriStyle-specific descriptions (culturally relevant) ────────

        private static string GetDescription(FaceShape shape) => shape switch
        {
            FaceShape.Oval => "You have an oval face — the most versatile shape. Almost any style suits you.",
            FaceShape.Round => "Your face is circular with soft angles. Styles that add height and length suit you best.",
            FaceShape.Square => "You have a strong jawline with balanced proportions. Soft volume on top works beautifully.",
            FaceShape.Heart => "Wider forehead, narrow chin. Volume at the jaw level creates beautiful balance.",
            FaceShape.Diamond => "Wide cheekbones are your standout feature. Styles with width at forehead or jaw complement you.",
            FaceShape.Oblong => "Your face is longer than wide. Volume at the sides and fullness mid-length flatter you most.",
            FaceShape.Triangle => "Strong jaw, narrower forehead. Volume at the crown brings everything into balance.",
            _ => "A unique face shape. We've found styles that will work beautifully for you."
        };

        private static string GetAfriStyleTip(FaceShape shape) => shape switch
        {
            FaceShape.Oval => "Try a high Afro puff, freeform locs, or any braid style — you can carry them all!",
            FaceShape.Round => "A high puff or pineapple updo adds height. Long knotless braids elongate beautifully.",
            FaceShape.Square => "Soft twists or loose Afro curls soften a strong jaw. Avoid very straight-edged cuts.",
            FaceShape.Heart => "Chin-length knotless braids or a full Afro with volume at the ears balances your shape.",
            FaceShape.Diamond => "Full bantu knots or a blowout with forward volume at the forehead works brilliantly.",
            FaceShape.Oblong => "A wide Afro, chunky box braids, or Bantu knots add width and visual balance.",
            FaceShape.Triangle => "High top fade, Afro, or cornrows with a voluminous top draw the eye upward perfectly.",
            _ => "Experiment! Your unique shape gives you flexibility to pull off bold styles."
        };
    }
}
