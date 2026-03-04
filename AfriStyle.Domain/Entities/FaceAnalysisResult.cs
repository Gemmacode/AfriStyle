using AfriStyle.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Domain.Entities
{
    public class FaceAnalysisResult
    {
        public FaceShape FaceShape { get; set; } = FaceShape.Unknown;
        public double Confidence { get; set; } = 0.0;                 

        // Optional: raw measurements (can be useful for debugging or advanced scoring)
        public double FaceLength { get; set; }
        public double FaceWidth { get; set; }
        public double ForeheadWidth { get; set; }
        public double CheekboneWidth { get; set; }
        public double JawWidth { get; set; }

        public bool IsValid => FaceShape != FaceShape.Unknown && Confidence > 0.5;
    }
}
