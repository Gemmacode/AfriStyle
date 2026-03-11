using AfriStyle.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Application.DTOs
{
    public record RecommendationResponseDto(
    string AnalysisId,
    FaceShape DetectedFaceShape,
    string FaceShapeName,
    double Confidence,
    string FaceShapeDescription,
    string FaceShapeTip,
    List<StyleResultDto> Recommendations
);
    
    public record StyleResultDto(
        Guid Id,
        string Name,
        string Description,
        string ImageUrl,
        string InspoImageUrl,
        string Category,
        int FitScore,
        string FitLabel,
        int FaceShapeScore,
        int TextureScore,
        int OccasionScore,
        bool IsProtectiveStyle,
        string MaintenanceLevel
    );
}
