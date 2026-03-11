using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Domain.ValueObjects
{
    public sealed record StyleFitScore
    {       
        public double FaceShapeScore { get; init; }        
        public double TextureScore { get; init; }       
        public double OccasionScore { get; init; }

        
        public double Total =>
            (FaceShapeScore * 0.50) +
            (TextureScore * 0.35) +
            (OccasionScore * 0.15);
        
        public int AsPercentage => (int)Math.Round(Total * 100);
        
        public string Label => AsPercentage switch
        {
            >= 90 => "Perfect Match",
            >= 75 => "Great Fit",
            >= 60 => "Good Fit",
            >= 45 => "Works for You",
            _ => "Worth Trying"
        };
    }
}
