using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Domain.ValueObjects
{
    public sealed record FaceMeasurements
    {        
        public double ForeheadWidth { get; init; }       
        public double CheekboneWidth { get; init; }        
        public double JawWidth { get; init; }        
        public double FaceLength { get; init; }        
        public double JawAngle { get; init; }

        // ── Derived ratios used by the classifier ──────────────────
        public double LengthToWidthRatio => FaceLength / CheekboneWidth;
        public double ForeheadToJawRatio => ForeheadWidth / JawWidth;
        public double CheekboneToForeheadRatio => CheekboneWidth / ForeheadWidth;
        public double CheekboneToJawRatio => CheekboneWidth / JawWidth;

        public FaceMeasurements(
            double foreheadWidth,
            double cheekboneWidth,
            double jawWidth,
            double faceLength,
            double jawAngle)
        {
            // Validation — prevents invalid data from entering the system
            if (foreheadWidth <= 0)
                throw new ArgumentException("Must be > 0", nameof(foreheadWidth));
            if (cheekboneWidth <= 0)
                throw new ArgumentException("Must be > 0", nameof(cheekboneWidth));
            if (jawWidth <= 0)
                throw new ArgumentException("Must be > 0", nameof(jawWidth));
            if (faceLength <= 0)
                throw new ArgumentException("Must be > 0", nameof(faceLength));

            ForeheadWidth = foreheadWidth;
            CheekboneWidth = cheekboneWidth;
            JawWidth = jawWidth;
            FaceLength = faceLength;
            JawAngle = Math.Clamp(jawAngle, 0, 1); 
        }
    }
}
