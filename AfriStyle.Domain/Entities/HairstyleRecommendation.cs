using AfriStyle.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Domain.Entities
{
    public class HairstyleRecommendation
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string ImageUrl { get; private set; } = string.Empty;
        public string InspoImageUrl { get; private set; } = string.Empty; // Celebrity/influencer reference

        public StyleCategory Category { get; private set; }
        public IReadOnlyList<FaceShape> SuitableFaceShapes { get; private set; } = Array.Empty<FaceShape>();
        public IReadOnlyList<HairTexture> SuitableTextures { get; private set; } = Array.Empty<HairTexture>();
        public IReadOnlyList<Occasion> SuitableOccasions { get; private set; } = Array.Empty<Occasion>();

        public MaintenanceLevel MaintenanceLevel { get; private set; }
        public bool IsProtectiveStyle { get; private set; }
        public bool SuitableForMen { get; private set; }
        public bool SuitableForWomen { get; private set; }

        // EF Core needs a parameterless constructor
        private HairstyleRecommendation() { }

        /// <summary>Factory method — the only way to create a valid hairstyle</summary>
        public static HairstyleRecommendation Create(
            string name,
            string description,
            string imageUrl,
            string inspoImageUrl,
            StyleCategory category,
            IEnumerable<FaceShape> faceShapes,
            IEnumerable<HairTexture> textures,
            IEnumerable<Occasion> occasions,
            MaintenanceLevel maintenanceLevel,
            bool isProtective = false,
            bool forMen = true,
            bool forWomen = true)
        {
            return new HairstyleRecommendation
            {
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                InspoImageUrl = inspoImageUrl,
                Category = category,
                SuitableFaceShapes = faceShapes.ToList().AsReadOnly(),
                SuitableTextures = textures.ToList().AsReadOnly(),
                SuitableOccasions = occasions.ToList().AsReadOnly(),
                MaintenanceLevel = maintenanceLevel,
                IsProtectiveStyle = isProtective,
                SuitableForMen = forMen,
                SuitableForWomen = forWomen
            };
        }
    }    
}
