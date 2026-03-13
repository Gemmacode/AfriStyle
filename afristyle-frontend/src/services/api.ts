import axios from 'axios';
import type { RecommendationRequest, RecommendationResponse } from '../types';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5214';

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const getRecommendations = async (
  request: RecommendationRequest
): Promise<RecommendationResponse> => {
  const response = await apiClient.post<RecommendationResponse>(
    '/api/recommendations',
    request
  );
  return response.data;
};

export default {
  getRecommendations,
};