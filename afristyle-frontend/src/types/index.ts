// Enums matching your .NET backend exactly

export enum FaceShape {
  Unknown = 0,
  Oval = 1,
  Round = 2,
  Square = 3,
  Heart = 4,
  Diamond = 5,
  Oblong = 6,
  Triangle = 7,
}

export enum HairTexture {
  Unknown = 0,
  Straight = 1,
  WavyLoose = 2,
  WavyCoarse = 3,
  CurlyLoose = 4,
  CurlyCorkscrew = 5,
  CoilyLoose = 6,
  CoilyZigzag = 7,
  CoilyTight = 8,
}

export enum Occasion {
  Unknown = 0,
  Everyday = 1,
  Office = 2,
  Party = 3,
  Church = 4,
  Wedding = 5,
  Gym = 6,
  Travel = 7,
}

export interface RecommendationRequest {
  foreheadWidth: number;
  cheekboneWidth: number;
  jawWidth: number;
  faceLength: number;
  jawAngle: number;
  hairTexture: HairTexture;
  occasion: Occasion;
  forMen: boolean;
}

export interface StyleResult {
  id: string;
  name: string;
  description: string;
  imageUrl: string;
  inspoImageUrl: string;
  category: string;
  fitScore: number;
  fitLabel: string;
  faceShapeScore: number;
  textureScore: number;
  occasionScore: number;
  isProtectiveStyle: boolean;
  maintenanceLevel: string;
}

export interface RecommendationResponse {
  analysisId: string;
  detectedFaceShape: FaceShape;
  faceShapeName: string;
  confidence: number;
  faceShapeDescription: string;
  faceShapeTip: string;
  recommendations: StyleResult[];
}

export interface FaceMeasurements {
  foreheadWidth: number;
  cheekboneWidth: number;
  jawWidth: number;
  faceLength: number;
  jawAngle: number;
}