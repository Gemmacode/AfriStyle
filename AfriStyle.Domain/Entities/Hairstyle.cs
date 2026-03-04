using AfriStyle.Domain.Enums;

namespace AfriStyle.Domain.Entities;

public class Hairstyle
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;              
    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;           
    public string ThumbnailUrl { get; set; } = string.Empty;       
    // Compatibility rules — used for scoring
    public List<FaceShape> SuitableFaceShapes { get; set; } = new();
    public List<HairTexture> SuitableTextures { get; set; } = new();

    public MaintenanceLevel MaintenanceLevel { get; set; } = MaintenanceLevel.Medium;
    public GenderPreference GenderPreference { get; set; } = GenderPreference.Any;

    // Optional extra filters/tags
    public bool IsProtectiveStyle { get; set; }                    
    public bool RequiresHeat { get; set; }
    public bool NeedsExtensions { get; set; }
    public int AverageDurationMinutes { get; set; }                
    public decimal? AverageCostNgn { get; set; }                   

    // For future: popularity, region, season suitability, etc.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}