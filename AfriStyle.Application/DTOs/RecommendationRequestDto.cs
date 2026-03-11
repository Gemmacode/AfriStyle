using AfriStyle.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Application.DTOs
{
    public record RecommendationRequestDto(
     // Face measurements from MediaPipe
     double ForeheadWidth,
     double CheekboneWidth,
     double JawWidth,
     double FaceLength,
     double JawAngle,

     // User preferences
     HairTexture HairTexture,
     Occasion Occasion,
     bool ForMen = false
 );
}
