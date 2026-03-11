using AfriStyle.Domain.Entities;
using AfriStyle.Domain.ValueObjects;
using Mapster;
using AfriStyle.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Application.Mappings
{
    public static class MappingConfig
    {
        public static void Configure()
        {
            // Map (HairstyleRecommendation + StyleFitScore) → StyleResultDto
            TypeAdapterConfig<(HairstyleRecommendation Style, StyleFitScore Score), StyleResultDto>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Style.Id)
                .Map(dest => dest.Name, src => src.Style.Name)
                .Map(dest => dest.Description, src => src.Style.Description)
                .Map(dest => dest.ImageUrl, src => src.Style.ImageUrl)
                .Map(dest => dest.InspoImageUrl, src => src.Style.InspoImageUrl)
                .Map(dest => dest.Category, src => src.Style.Category.ToString().Replace("_", " "))
                .Map(dest => dest.FitScore, src => src.Score.AsPercentage)
                .Map(dest => dest.FitLabel, src => src.Score.Label)
                .Map(dest => dest.FaceShapeScore, src => (int)Math.Round(src.Score.FaceShapeScore * 100))
                .Map(dest => dest.TextureScore, src => (int)Math.Round(src.Score.TextureScore * 100))
                .Map(dest => dest.OccasionScore, src => (int)Math.Round(src.Score.OccasionScore * 100))
                .Map(dest => dest.IsProtectiveStyle, src => src.Style.IsProtectiveStyle)
                .Map(dest => dest.MaintenanceLevel, src => src.Style.MaintenanceLevel.ToString());
        }
    }
}
