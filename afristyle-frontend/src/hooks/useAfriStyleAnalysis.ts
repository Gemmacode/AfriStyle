import { useState } from 'react';
import { useFaceLandmarker } from './useFaceLandmarker';
import { getRecommendations } from '../services/api';
import type { HairTexture, Occasion, RecommendationResponse } from '../types';

export const useAfriStyleAnalysis = () => {
  const { detectFace, isLoading: isLoadingMediaPipe, error: mediaPipeError } = useFaceLandmarker();
  const [isAnalyzing, setIsAnalyzing] = useState(false);
  const [results, setResults] = useState<RecommendationResponse | null>(null);
  const [error, setError] = useState<string | null>(null);

  const analyzePhoto = async (
    image: HTMLImageElement,
    hairTexture: HairTexture,
    occasion: Occasion,
    forMen: boolean
  ) => {
    try {
      setIsAnalyzing(true);
      setError(null);

      // Step 1: Extract face measurements using MediaPipe
      const measurements = await detectFace(image);

      if (!measurements) {
        setError('Could not detect face. Please try a different photo.');
        setIsAnalyzing(false);
        return;
      }

      console.log('Face measurements extracted:', measurements);

      // Step 2: Call your .NET API
      const response = await getRecommendations({
        foreheadWidth: measurements.foreheadWidth,
        cheekboneWidth: measurements.cheekboneWidth,
        jawWidth: measurements.jawWidth,
        faceLength: measurements.faceLength,
        jawAngle: measurements.jawAngle,
        hairTexture,
        occasion,
        forMen,
      });

      console.log('API response:', response);

      // Step 3: Set results
      setResults(response);
      setIsAnalyzing(false);
    } catch (err: any) {
      console.error('Analysis error:', err);
      setError(
        err.response?.data?.error || 
        'Failed to get recommendations. Please ensure the API is running.'
      );
      setIsAnalyzing(false);
    }
  };

  const reset = () => {
    setResults(null);
    setError(null);
  };

  return {
    analyzePhoto,
    reset,
    results,
    isAnalyzing,
    isLoadingMediaPipe,
    error: error || mediaPipeError,
  };
};