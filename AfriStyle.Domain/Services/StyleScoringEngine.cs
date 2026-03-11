using AfriStyle.Domain.Entities;
using AfriStyle.Domain.Enums;
using AfriStyle.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Domain.Services
{
    public class StyleScoringEngine
    {
        public StyleFitScore Score(
            HairstyleRecommendation style,
            FaceShape userFaceShape,
            HairTexture userTexture,
            Occasion userOccasion)
        {
            return new StyleFitScore
            {
                FaceShapeScore = ScoreFaceShape(style, userFaceShape),
                TextureScore = ScoreTexture(style, userTexture),
                OccasionScore = ScoreOccasion(style, userOccasion),
            };
        }

        private static double ScoreFaceShape(HairstyleRecommendation style, FaceShape shape)
        {
            if (!style.SuitableFaceShapes.Any()) return 0.6; // neutral if unspecified
            if (style.SuitableFaceShapes.Contains(shape)) return 1.0;

            // Partial score for "adjacent" shapes that still work reasonably
            var adjacentShapes = GetAdjacentShapes(shape);
            return style.SuitableFaceShapes.Any(s => adjacentShapes.Contains(s)) ? 0.55 : 0.20;
        }

        private static double ScoreTexture(HairstyleRecommendation style, HairTexture texture)
        {
            if (!style.SuitableTextures.Any()) return 0.6;
            if (style.SuitableTextures.Contains(texture)) return 1.0;

            // Coily textures are adjacent — 4B and 4C are close enough for most styles
            var adjacent = GetAdjacentTextures(texture);
            return style.SuitableTextures.Any(t => adjacent.Contains(t)) ? 0.65 : 0.25;
        }

        private static double ScoreOccasion(HairstyleRecommendation style, Occasion occasion)
        {
            if (!style.SuitableOccasions.Any()) return 0.6;
            return style.SuitableOccasions.Contains(occasion) ? 1.0 : 0.3;
        }

        // ── Adjacent shape logic (shapes that work similarly) ────────

        private static IEnumerable<FaceShape> GetAdjacentShapes(FaceShape shape) => shape switch
        {
            FaceShape.Oval => new[] { FaceShape.Round, FaceShape.Oblong },
            FaceShape.Round => new[] { FaceShape.Oval, FaceShape.Square },
            FaceShape.Square => new[] { FaceShape.Round, FaceShape.Oblong },
            FaceShape.Heart => new[] { FaceShape.Oval, FaceShape.Diamond },
            FaceShape.Diamond => new[] { FaceShape.Heart, FaceShape.Oval },
            FaceShape.Oblong => new[] { FaceShape.Oval, FaceShape.Square },
            FaceShape.Triangle => new[] { FaceShape.Square, FaceShape.Round },
            _ => Array.Empty<FaceShape>()
        };

        // ── Adjacent texture logic (textures that behave similarly) ──

        private static IEnumerable<HairTexture> GetAdjacentTextures(HairTexture texture) => texture switch
        {
            HairTexture.CoilyTight => new[] { HairTexture.CoilyZigzag },
            HairTexture.CoilyZigzag => new[] { HairTexture.CoilyTight, HairTexture.CoilyLoose },
            HairTexture.CoilyLoose => new[] { HairTexture.CoilyZigzag, HairTexture.CurlyCorkscrew },
            HairTexture.CurlyCorkscrew => new[] { HairTexture.CoilyLoose, HairTexture.CurlyLoose },
            HairTexture.CurlyLoose => new[] { HairTexture.CurlyCorkscrew, HairTexture.WavyCoarse },
            _ => Array.Empty<HairTexture>()
        };
    }
}
