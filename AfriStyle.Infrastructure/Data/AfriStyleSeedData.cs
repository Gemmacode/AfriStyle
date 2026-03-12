using AfriStyle.Domain.Entities;
using AfriStyle.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Infrastructure.Data
{
    public static class AfriStyleSeedData
    {
        public static List<HairstyleRecommendation> GetAll() =>
        [
            // ── PROTECTIVE STYLES (Women) ────────────────────────────

            HairstyleRecommendation.Create(
            name: "Knotless Box Braids (Waist Length)",
            description: "The ultimate protective style. Lighter than traditional box braids with less tension at the root. Waist length adds dramatic elongation perfect for round and square faces.",
            imageUrl: "/images/styles/knotless-waist.jpg",
            inspoImageUrl: "/images/inspo/knotless-waist-inspo.jpg",
            category: StyleCategory.KnotlessBraids,
            faceShapes: [FaceShape.Round, FaceShape.Square, FaceShape.Oblong, FaceShape.Triangle],
            textures: [HairTexture.CoilyTight, HairTexture.CoilyZigzag, HairTexture.CoilyLoose],
            occasions: [Occasion.Everyday, Occasion.Office, Occasion.Church, Occasion.Travel],
            maintenanceLevel: MaintenanceLevel.Low,
            isProtective: true,
            forMen: false,
            forWomen: true
        ),

        HairstyleRecommendation.Create(
            name: "Knotless Box Braids (Shoulder Length)",
            description: "The most requested protective style in Nigerian salons right now. Clean, modern, and incredibly versatile. Perfect for oval and heart-shaped faces.",
            imageUrl: "/images/styles/knotless-shoulder.jpg",
            inspoImageUrl: "/images/inspo/knotless-shoulder-inspo.jpg",
            category: StyleCategory.KnotlessBraids,
            faceShapes: [FaceShape.Oval, FaceShape.Heart, FaceShape.Diamond, FaceShape.Oblong],
            textures: [HairTexture.CoilyTight, HairTexture.CoilyZigzag, HairTexture.CoilyLoose],
            occasions: [Occasion.Everyday, Occasion.Office, Occasion.Party],
            maintenanceLevel: MaintenanceLevel.Low,
            isProtective: true,
            forMen: false,
            forWomen: true
        ),

        HairstyleRecommendation.Create(
            name: "Faux Locs (Bohemian Style)",
            description: "Bohemian faux locs give a free-spirited, laid-back look with unravelled ends for texture. Lasts 6-8 weeks with minimal maintenance.",
            imageUrl: "/images/styles/faux-locs.jpg",
            inspoImageUrl: "/images/inspo/faux-locs-inspo.jpg",
            category: StyleCategory.FauxLocs,
            faceShapes: [FaceShape.Oval, FaceShape.Round, FaceShape.Square, FaceShape.Heart],
            textures: [HairTexture.CoilyTight, HairTexture.CoilyZigzag, HairTexture.CoilyLoose, HairTexture.CurlyCorkscrew],
            occasions: [Occasion.Everyday, Occasion.Party, Occasion.Travel],
            maintenanceLevel: MaintenanceLevel.Low,
            isProtective: true,
            forMen: false,
            forWomen: true
        ),

        HairstyleRecommendation.Create(
            name: "Ghana Braids (Straight Back)",
            description: "Classic feed-in cornrows straight to the back. A timeless Nigerian go-to for clean, professional looks. Works for all genders.",
            imageUrl: "/images/styles/ghana-braids.jpg",
            inspoImageUrl: "/images/inspo/ghana-braids-inspo.jpg",
            category: StyleCategory.Cornrows,
            faceShapes: [FaceShape.Oval, FaceShape.Square, FaceShape.Oblong],
            textures: [HairTexture.CoilyTight, HairTexture.CoilyZigzag, HairTexture.CoilyLoose],
            occasions: [Occasion.Everyday, Occasion.Office, Occasion.Church],
            maintenanceLevel: MaintenanceLevel.Low,
            isProtective: true,
            forMen: true,
            forWomen: true
        ),

        HairstyleRecommendation.Create(
            name: "Bantu Knots",
            description: "Iconic, bold, and deeply rooted in African culture. Each knot is a statement. Works beautifully as a style or as prep for a twist-out.",
            imageUrl: "/images/styles/bantu-knots.jpg",
            inspoImageUrl: "/images/inspo/bantu-knots-inspo.jpg",
            category: StyleCategory.BantuKnots,
            faceShapes: [FaceShape.Oblong, FaceShape.Diamond, FaceShape.Triangle],
            textures: [HairTexture.CoilyTight, HairTexture.CoilyZigzag],
            occasions: [Occasion.Party, Occasion.Everyday, Occasion.Church],
            maintenanceLevel: MaintenanceLevel.Medium,
            isProtective: true,
            forMen: false,
            forWomen: true
        ),

        HairstyleRecommendation.Create(
            name: "Senegalese Twists",
            description: "Rope twists using Kanekalon hair. Lighter and neater than braids. A top pick for 4C hair types wanting length protection.",
            imageUrl: "/images/styles/senegalese-twists.jpg",
            inspoImageUrl: "/images/inspo/senegalese-twists-inspo.jpg",
            category: StyleCategory.Twists,
            faceShapes: [FaceShape.Round, FaceShape.Square, FaceShape.Heart, FaceShape.Oval],
            textures: [HairTexture.CoilyTight, HairTexture.CoilyZigzag, HairTexture.CoilyLoose],
            occasions: [Occasion.Everyday, Occasion.Office, Occasion.Church, Occasion.Travel],
            maintenanceLevel: MaintenanceLevel.Low,
            isProtective: true,
            forMen: false,
            forWomen: true
        ),

        // ── NATURAL STYLES ────────────────────────────────────────

        HairstyleRecommendation.Create(
            name: "High Afro Puff",
            description: "The high puff is the ultimate crown for 4C hair. Maximum volume at the top, clean sides. Regal, bold, unmistakably African.",
            imageUrl: "/images/styles/high-afro-puff.jpg",
            inspoImageUrl: "/images/inspo/high-puff-inspo.jpg",
            category: StyleCategory.Afro,
            faceShapes: [FaceShape.Round, FaceShape.Square, FaceShape.Triangle, FaceShape.Oblong],
            textures: [HairTexture.CoilyTight, HairTexture.CoilyZigzag, HairTexture.CoilyLoose],
            occasions: [Occasion.Everyday, Occasion.Party, Occasion.Church],
            maintenanceLevel: MaintenanceLevel.Medium,
            isProtective: false,
            forMen: false,
            forWomen: true
        ),

        HairstyleRecommendation.Create(
            name: "Freestanding Afro",
            description: "Fully picked out natural Afro. Volume in every direction. A power statement — especially on 4B/4C textures.",
            imageUrl: "/images/styles/freestanding-afro.jpg",
            inspoImageUrl: "/images/inspo/afro-inspo.jpg",
            category: StyleCategory.Afro,
            faceShapes: [FaceShape.Oblong, FaceShape.Heart, FaceShape.Triangle, FaceShape.Diamond],
            textures: [HairTexture.CoilyTight, HairTexture.CoilyZigzag, HairTexture.CoilyLoose, HairTexture.CurlyCorkscrew],
            occasions: [Occasion.Party, Occasion.Church, Occasion.Everyday],
            maintenanceLevel: MaintenanceLevel.Medium,
            isProtective: false,
            forMen: true,
            forWomen: true
        ),

        HairstyleRecommendation.Create(
            name: "TWA (Teeny Weeny Afro)",
            description: "The TWA is bold, liberating, and frames every face shape differently. A celebration of natural coily texture at its most raw.",
            imageUrl: "/images/styles/twa.jpg",
            inspoImageUrl: "/images/inspo/twa-inspo.jpg",
            category: StyleCategory.TWA,
            faceShapes: [FaceShape.Oval, FaceShape.Heart, FaceShape.Diamond],
            textures: [HairTexture.CoilyTight, HairTexture.CoilyZigzag],
            occasions: [Occasion.Everyday, Occasion.Gym, Occasion.Travel],
            maintenanceLevel: MaintenanceLevel.Low,
            isProtective: false,
            forMen: false,
            forWomen: true
        ),

        // ── MEN'S STYLES ──────────────────────────────────────────

        HairstyleRecommendation.Create(
            name: "Low Fade with Design",
            description: "A crisp low fade with a custom design cut into the side. The most popular barbershop request across Lagos, Abuja, and Port Harcourt.",
            imageUrl: "/images/styles/low-fade-design.jpg",
            inspoImageUrl: "/images/inspo/low-fade-inspo.jpg",
            category: StyleCategory.Fade,
            faceShapes: [FaceShape.Oval, FaceShape.Square, FaceShape.Round],
            textures: [HairTexture.CoilyTight, HairTexture.CoilyZigzag, HairTexture.CoilyLoose],
            occasions: [Occasion.Everyday, Occasion.Office, Occasion.Party],
            maintenanceLevel: MaintenanceLevel.Medium,
            isProtective: false,
            forMen: true,
            forWomen: false
        ),

        HairstyleRecommendation.Create(
            name: "High Top Fade",
            description: "Flat top with high skin fade. Adds significant height and visual width at the crown — great for men with longer face shapes.",
            imageUrl: "/images/styles/high-top-fade.jpg",
            inspoImageUrl: "/images/inspo/high-top-inspo.jpg",
            category: StyleCategory.TempleFlattop,
            faceShapes: [FaceShape.Oblong, FaceShape.Triangle, FaceShape.Round],
            textures: [HairTexture.CoilyTight, HairTexture.CoilyZigzag],
            occasions: [Occasion.Everyday, Occasion.Party],
            maintenanceLevel: MaintenanceLevel.Medium,
            isProtective: false,
            forMen: true,
            forWomen: false
        ),

        HairstyleRecommendation.Create(
            name: "Dreadlocks (Starter Locs)",
            description: "Beginning your loc journey. Fresh starter locs look clean, professional, and grow into a deeply personal style over time.",
            imageUrl: "/images/styles/starter-locs.jpg",
            inspoImageUrl: "/images/inspo/locs-inspo.jpg",
            category: StyleCategory.Dreadlocks,
            faceShapes: [FaceShape.Oval, FaceShape.Square, FaceShape.Diamond, FaceShape.Oblong],
            textures: [HairTexture.CoilyTight, HairTexture.CoilyZigzag, HairTexture.CoilyLoose],
            occasions: [Occasion.Everyday, Occasion.Office, Occasion.Travel],
            maintenanceLevel: MaintenanceLevel.Low,
            isProtective: true,
            forMen: true,
            forWomen: true
        ),

        // ── SPECIAL OCCASIONS ─────────────────────────────────────

        HairstyleRecommendation.Create(
            name: "Aso Ebi Braided Updo",
            description: "Elaborate braided updo perfect for Nigerian traditional weddings and Aso Ebi events. Embellished with gold cuffs or cowrie shells.",
            imageUrl: "/images/styles/aso-ebi-updo.jpg",
            inspoImageUrl: "/images/inspo/aso-ebi-inspo.jpg",
            category: StyleCategory.WeddingUpdo,
            faceShapes: [FaceShape.Oval, FaceShape.Heart, FaceShape.Diamond, FaceShape.Round],
            textures: [HairTexture.CoilyTight, HairTexture.CoilyZigzag, HairTexture.CoilyLoose],
            occasions: [Occasion.Wedding, Occasion.Church, Occasion.Party],
            maintenanceLevel: MaintenanceLevel.High,
            isProtective: true,
            forMen: false,
            forWomen: true
        ),

        HairstyleRecommendation.Create(
            name: "Low Cut (Buzz Cut)",
            description: "Clean, simple, low-maintenance. The go-to for busy professionals and athletes. Works with any face shape.",
            imageUrl: "/images/styles/low-cut.jpg",
            inspoImageUrl: "/images/inspo/low-cut-inspo.jpg",
            category: StyleCategory.LowCut,
            faceShapes: [FaceShape.Oval, FaceShape.Square, FaceShape.Round, FaceShape.Heart, FaceShape.Diamond, FaceShape.Oblong, FaceShape.Triangle],
            textures: [HairTexture.CoilyTight, HairTexture.CoilyZigzag, HairTexture.CoilyLoose],
            occasions: [Occasion.Everyday, Occasion.Office, Occasion.Gym, Occasion.Travel],
            maintenanceLevel: MaintenanceLevel.Low,
            isProtective: false,
            forMen: true,
            forWomen: false
        ),
    ];
    }
}
