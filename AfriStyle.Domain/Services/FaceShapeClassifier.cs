using AfriStyle.Domain.Enums;
using AfriStyle.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Domain.Services
{
    public class FaceShapeClassifier
    {
        private const double EqualityThreshold = 0.10; 
        
        public (FaceShape Shape, double Confidence) Classify(FaceMeasurements m)
        {
            // Score each possible shape — highest scorer wins
            var scores = new Dictionary<FaceShape, double>
            {
                [FaceShape.Oval] = ScoreOval(m),
                [FaceShape.Round] = ScoreRound(m),
                [FaceShape.Square] = ScoreSquare(m),
                [FaceShape.Heart] = ScoreHeart(m),
                [FaceShape.Diamond] = ScoreDiamond(m),
                [FaceShape.Oblong] = ScoreOblong(m),
                [FaceShape.Triangle] = ScoreTriangle(m),
            };

            var best = scores.MaxBy(kv => kv.Value);
            return (best.Key, Math.Round(best.Value, 2));
        }        

        private static double ScoreOval(FaceMeasurements m)
        {            
            double score = 0;
            if (m.LengthToWidthRatio is > 1.25 and < 1.60) score += 0.35;
            if (m.CheekboneWidth > m.ForeheadWidth) score += 0.25;
            if (m.CheekboneWidth > m.JawWidth) score += 0.25;
            if (m.ForeheadToJawRatio is > 1.0 and < 1.3) score += 0.15;
            return score;
        }

        private static double ScoreRound(FaceMeasurements m)
        {            
            double score = 0;
            if (m.LengthToWidthRatio is > 0.9 and < 1.15) score += 0.40;
            if (m.JawAngle < 0.3) score += 0.30; 
            if (IsApprox(m.CheekboneWidth, m.ForeheadWidth, 0.12)) score += 0.30;
            return score;
        }

        private static double ScoreSquare(FaceMeasurements m)
        {            
            double score = 0;
            if (m.LengthToWidthRatio is > 0.95 and < 1.20) score += 0.25;
            if (m.JawAngle > 0.65) score += 0.40; 
            if (IsApprox(m.ForeheadWidth, m.JawWidth, EqualityThreshold)) score += 0.35;
            return score;
        }

        private static double ScoreHeart(FaceMeasurements m)
        {            
            double score = 0;
            if (m.ForeheadToJawRatio > 1.3) score += 0.50;
            if (m.CheekboneWidth >= m.ForeheadWidth) score += 0.25;
            if (m.JawAngle < 0.4) score += 0.25; 
            return score;
        }

        private static double ScoreDiamond(FaceMeasurements m)
        {            
            double score = 0;
            if (m.CheekboneToForeheadRatio > 1.20) score += 0.35;
            if (m.CheekboneToJawRatio > 1.20) score += 0.35;
            if (m.LengthToWidthRatio is > 1.1 and < 1.5) score += 0.30;
            return score;
        }

        private static double ScoreOblong(FaceMeasurements m)
        {            
            double score = 0;
            if (m.LengthToWidthRatio > 1.6) score += 0.50;
            if (IsApprox(m.ForeheadWidth, m.JawWidth, EqualityThreshold)) score += 0.30;
            if (IsApprox(m.CheekboneWidth, m.ForeheadWidth, 0.12)) score += 0.20;
            return score;
        }

        private static double ScoreTriangle(FaceMeasurements m)
        {            
            double score = 0;
            if (m.ForeheadToJawRatio < 0.85) score += 0.60;
            if (m.JawWidth > m.CheekboneWidth) score += 0.40;
            return score;
        }

        private static bool IsApprox(double a, double b, double tolerance)
            => Math.Abs(a - b) / Math.Max(a, b) <= tolerance;
    }
}
