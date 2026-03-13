import { useEffect, useRef, useState } from 'react';
import { FaceLandmarker, FilesetResolver } from '@mediapipe/tasks-vision';
import type { FaceMeasurements } from '../types';

/**
 * MediaPipe Face Landmarker Hook
 * Detects 478 facial landmarks and extracts the 5 measurements we need
 * Runs 100% in browser - no photo upload to server needed
 */
export const useFaceLandmarker = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const faceLandmarkerRef = useRef<FaceLandmarker | null>(null);

  // Initialize MediaPipe on component mount
  useEffect(() => {
    const initializeFaceLandmarker = async () => {
      try {
        setIsLoading(true);

        // Load MediaPipe WASM files from CDN
        const vision = await FilesetResolver.forVisionTasks(
          'https://cdn.jsdelivr.net/npm/@mediapipe/tasks-vision@0.10.0/wasm'
        );

        // Create face landmarker with the model
        const landmarker = await FaceLandmarker.createFromOptions(vision, {
          baseOptions: {
            modelAssetPath:
              'https://storage.googleapis.com/mediapipe-models/face_landmarker/face_landmarker/float16/1/face_landmarker.task',
            delegate: 'GPU', // Use GPU acceleration
          },
          runningMode: 'IMAGE',
          numFaces: 1, // Only detect one face
        });

        faceLandmarkerRef.current = landmarker;
        setIsLoading(false);
      } catch (err) {
        console.error('MediaPipe initialization error:', err);
        setError('Failed to load face detection model. Please refresh the page.');
        setIsLoading(false);
      }
    };

    initializeFaceLandmarker();

    // Cleanup
    return () => {
      if (faceLandmarkerRef.current) {
        faceLandmarkerRef.current.close();
      }
    };
  }, []);

  /**
   * Extract face measurements from an image
   */
  const detectFace = async (imageElement: HTMLImageElement): Promise<FaceMeasurements | null> => {
    if (!faceLandmarkerRef.current) {
      setError('Face detector not initialized');
      return null;
    }

    try {
      // Detect face landmarks
      const results = faceLandmarkerRef.current.detect(imageElement);

      if (!results.faceLandmarks || results.faceLandmarks.length === 0) {
        setError('No face detected. Please use a clear front-facing photo.');
        return null;
      }

      // Get the 478 facial landmarks (x, y, z coordinates)
      const landmarks = results.faceLandmarks[0];

      // MediaPipe landmark indices we need:
      const FOREHEAD_TOP = 10;
      const CHIN = 152;
      const LEFT_CHEEK = 234;
      const RIGHT_CHEEK = 454;
      const LEFT_JAW = 172;
      const RIGHT_JAW = 397;
      const LEFT_TEMPLE = 67;
      const RIGHT_TEMPLE = 297;

      // Calculate distances
      const foreheadWidth = Math.abs(landmarks[LEFT_TEMPLE].x - landmarks[RIGHT_TEMPLE].x);
      const cheekboneWidth = Math.abs(landmarks[LEFT_CHEEK].x - landmarks[RIGHT_CHEEK].x);
      const jawWidth = Math.abs(landmarks[LEFT_JAW].x - landmarks[RIGHT_JAW].x);
      const faceLength = Math.abs(landmarks[FOREHEAD_TOP].y - landmarks[CHIN].y);

      // Calculate jaw angle (0 = very round, 1 = very sharp)
      const jawAngle = calculateJawAngle(landmarks, LEFT_JAW, RIGHT_JAW, CHIN);

      // Normalize measurements (cheekbone is usually the widest point)
      const normalizationFactor = cheekboneWidth;

      const measurements: FaceMeasurements = {
        foreheadWidth: foreheadWidth / normalizationFactor,
        cheekboneWidth: 1.0, // Normalized to 1.0
        jawWidth: jawWidth / normalizationFactor,
        faceLength: faceLength / normalizationFactor,
        jawAngle: Math.max(0, Math.min(1, jawAngle)), // Clamp 0-1
      };

      setError(null);
      return measurements;
    } catch (err) {
      console.error('Face detection error:', err);
      setError('Failed to detect face. Please try a different photo.');
      return null;
    }
  };

  return {
    detectFace,
    isLoading,
    error,
  };
};

/**
 * Calculate jaw angle from landmarks
 * Returns 0-1 where 0 = very round/soft jaw, 1 = very sharp/angular jaw
 */
function calculateJawAngle(
  landmarks: any[],
  leftJawIndex: number,
  rightJawIndex: number,
  chinIndex: number
): number {
  const leftJaw = landmarks[leftJawIndex];
  const rightJaw = landmarks[rightJawIndex];
  const chin = landmarks[chinIndex];

  // Calculate angle between jaw points and chin
  const leftAngle = Math.atan2(chin.y - leftJaw.y, chin.x - leftJaw.x);
  const rightAngle = Math.atan2(chin.y - rightJaw.y, chin.x - rightJaw.x);

  // Convert to 0-1 scale (sharper jaw = higher value)
  const angleDiff = Math.abs(leftAngle - rightAngle);
  return Math.min(1, angleDiff / Math.PI);
}