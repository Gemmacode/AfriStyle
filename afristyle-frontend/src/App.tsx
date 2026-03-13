import { useState } from 'react';
import { PhotoCapture } from './components/PhotoCapture';
import { PreferencesForm } from './components/PreferencesForm';
import { Results } from './components/Results';
import { useAfriStyleAnalysis } from './hooks/useAfriStyleAnalysis';
import { HairTexture, Occasion } from './types';

type Step = 'upload' | 'preferences' | 'results';

function App() {
  const [currentStep, setCurrentStep] = useState<Step>('upload');
  const [capturedImage, setCapturedImage] = useState<HTMLImageElement | null>(null);

  // User preferences
  const [hairTexture, setHairTexture] = useState<HairTexture>(HairTexture.CoilyTight);
  const [occasion, setOccasion] = useState<Occasion>(Occasion.Everyday);
  const [forMen, setForMen] = useState(false);

  const { analyzePhoto, reset, results, isAnalyzing, isLoadingMediaPipe, error } =
    useAfriStyleAnalysis();

  const handlePhotoCapture = (image: HTMLImageElement) => {
    setCapturedImage(image);
    setCurrentStep('preferences');
  };

  const handleGetRecommendations = async () => {
    if (!capturedImage) return;

    await analyzePhoto(capturedImage, hairTexture, occasion, forMen);

    if (!error) {
      setCurrentStep('results');
    }
  };

  const handleStartOver = () => {
    setCapturedImage(null);
    setCurrentStep('upload');
    reset();
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-kente-gold/10 via-earth-light to-kente-orange/10 relative overflow-hidden">
      {/* Background Pattern */}
      <div className="absolute inset-0 opacity-5">
        <div className="absolute top-20 left-10 w-32 h-32 bg-kente-gold rounded-full blur-3xl"></div>
        <div className="absolute bottom-20 right-10 w-40 h-40 bg-kente-orange rounded-full blur-3xl"></div>
        <div className="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 w-60 h-60 bg-kente-brown/20 rounded-full blur-3xl"></div>
      </div>

      <div className="relative z-10">
        {/* Header */}
        <header className="text-center py-8 px-4">
          <div className="max-w-4xl mx-auto">
            <div className="flex items-center justify-center gap-3 mb-4">
              <div className="w-12 h-12 bg-gradient-to-br from-kente-gold to-kente-orange rounded-full flex items-center justify-center shadow-lg">
                <span className="text-2xl">👑</span>
              </div>
              <h1 className="text-4xl md:text-5xl font-bold bg-gradient-to-r from-kente-brown via-kente-orange to-kente-red bg-clip-text text-transparent">
                AfriStyle Fit
              </h1>
            </div>
            <p className="text-lg md:text-xl text-earth-dark font-medium mb-2">
              ✨ AI-Powered Hairstyle Recommendations for African & Textured Hair
            </p>
            <div className="flex flex-wrap justify-center gap-3 text-sm text-gray-600">
              <span className="px-4 py-2 bg-white/80 backdrop-blur-sm rounded-full shadow-sm border border-kente-gold/20">
                🇳🇬 Made in Nigeria
              </span>
              <span className="px-4 py-2 bg-white/80 backdrop-blur-sm rounded-full shadow-sm border border-kente-gold/20">
                🔒 Privacy-First
              </span>
              <span className="px-4 py-2 bg-white/80 backdrop-blur-sm rounded-full shadow-sm border border-kente-gold/20">
                ⚡ Instant Results
              </span>
              <span className="px-4 py-2 bg-white/80 backdrop-blur-sm rounded-full shadow-sm border border-kente-gold/20">
                🎨 AI-Powered
              </span>
            </div>
          </div>
        </header>

        {/* Progress Indicator */}
        <div className="max-w-2xl mx-auto px-4 mb-8">
          <div className="flex items-center justify-center gap-2">
            <div className={`w-3 h-3 rounded-full transition-colors ${currentStep === 'upload' ? 'bg-kente-orange' : currentStep === 'preferences' || currentStep === 'results' ? 'bg-kente-gold' : 'bg-gray-300'}`}></div>
            <div className={`w-8 h-0.5 transition-colors ${currentStep === 'preferences' || currentStep === 'results' ? 'bg-kente-gold' : 'bg-gray-300'}`}></div>
            <div className={`w-3 h-3 rounded-full transition-colors ${currentStep === 'preferences' ? 'bg-kente-orange' : currentStep === 'results' ? 'bg-kente-gold' : 'bg-gray-300'}`}></div>
            <div className={`w-8 h-0.5 transition-colors ${currentStep === 'results' ? 'bg-kente-gold' : 'bg-gray-300'}`}></div>
            <div className={`w-3 h-3 rounded-full transition-colors ${currentStep === 'results' ? 'bg-kente-orange' : 'bg-gray-300'}`}></div>
          </div>
          <div className="flex justify-center gap-16 mt-2 text-sm font-medium text-gray-600">
            <span className={currentStep === 'upload' ? 'text-kente-orange' : ''}>Upload</span>
            <span className={currentStep === 'preferences' ? 'text-kente-orange' : ''}>Preferences</span>
            <span className={currentStep === 'results' ? 'text-kente-orange' : ''}>Results</span>
          </div>
        </div>

        {/* Loading State */}
        {isLoadingMediaPipe && (
          <div className="max-w-md mx-auto bg-white/90 backdrop-blur-sm rounded-2xl shadow-xl p-8 text-center border border-kente-gold/20">
            <div className="relative mb-6">
              <div className="w-16 h-16 border-4 border-kente-gold/30 border-t-kente-orange rounded-full animate-spin mx-auto"></div>
              <div className="absolute inset-0 w-16 h-16 border-4 border-transparent border-t-kente-red rounded-full animate-spin mx-auto" style={{animationDirection: 'reverse', animationDuration: '1.5s'}}></div>
            </div>
            <h3 className="text-xl font-semibold text-earth-dark mb-2">Loading AI Face Detection</h3>
            <p className="text-gray-600">Initializing advanced computer vision model...</p>
            <p className="text-sm text-gray-500 mt-2">This only happens once ✨</p>
          </div>
        )}

        {/* Error Display */}
        {error && (
          <div className="max-w-md mx-auto bg-red-50/90 backdrop-blur-sm border-2 border-red-200 rounded-2xl p-6 mb-6 shadow-lg">
            <div className="flex items-center gap-3 mb-3">
              <div className="w-10 h-10 bg-red-100 rounded-full flex items-center justify-center">
                <span className="text-red-600 text-xl">⚠️</span>
              </div>
              <h3 className="text-lg font-semibold text-red-800">Oops! Something went wrong</h3>
            </div>
            <p className="text-red-700">{error}</p>
            <button
              onClick={() => window.location.reload()}
              className="mt-4 px-4 py-2 bg-red-600 text-white rounded-lg hover:bg-red-700 transition-colors text-sm font-medium"
            >
              Try Again
            </button>
          </div>
        )}

        {/* Main Content */}
        {!isLoadingMediaPipe && (
          <>
            {/* Step 1: Photo Upload */}
            {currentStep === 'upload' && (
              <div className="max-w-2xl mx-auto px-4">
                <div className="bg-white/90 backdrop-blur-sm rounded-3xl shadow-2xl p-8 border border-kente-gold/20">
                  <div className="text-center mb-8">
                    <h2 className="text-3xl font-bold text-earth-dark mb-4">
                      Let's Find Your Perfect Style! 🌟
                    </h2>
                    <p className="text-gray-600 text-lg">
                      Upload a clear photo of yourself and let our AI analyze your face shape
                    </p>
                  </div>
                  <PhotoCapture onPhotoCapture={handlePhotoCapture} isProcessing={false} />
                </div>
              </div>
            )}

            {/* Step 2: Preferences */}
            {currentStep === 'preferences' && (
              <div className="max-w-2xl mx-auto px-4">
                <div className="bg-white/90 backdrop-blur-sm rounded-3xl shadow-2xl p-8 border border-kente-gold/20">
                  <div className="text-center mb-8">
                    <h2 className="text-3xl font-bold text-earth-dark mb-4">
                      Tell Us About Your Hair 💇🏾‍♀️
                    </h2>
                    <p className="text-gray-600 text-lg">
                      Help us personalize recommendations for your unique hair texture and lifestyle
                    </p>
                  </div>

                  <PreferencesForm
                    hairTexture={hairTexture}
                    occasion={occasion}
                    forMen={forMen}
                    onHairTextureChange={setHairTexture}
                    onOccasionChange={setOccasion}
                    onGenderChange={setForMen}
                  />

                  <div className="mt-10 flex gap-4">
                    <button
                      onClick={() => setCurrentStep('upload')}
                      className="flex-1 py-4 px-6 rounded-2xl font-semibold text-gray-700 bg-gray-100 hover:bg-gray-200 transition-all duration-200 transform hover:scale-105 active:scale-95 shadow-lg hover:shadow-xl border border-gray-200"
                    >
                      ← Back to Photo
                    </button>
                    <button
                      onClick={handleGetRecommendations}
                      disabled={isAnalyzing || hairTexture === HairTexture.Unknown}
                      className={`
                        flex-1 py-4 px-6 rounded-2xl font-semibold text-white text-lg
                        transition-all duration-200 transform
                        ${
                          isAnalyzing || hairTexture === HairTexture.Unknown
                            ? 'bg-gray-400 cursor-not-allowed'
                            : 'bg-gradient-to-r from-kente-orange to-kente-red hover:from-kente-red hover:to-kente-orange shadow-lg hover:shadow-xl hover:scale-105 active:scale-95'
                        }
                      `}
                    >
                      {isAnalyzing ? (
                        <span className="flex items-center justify-center gap-3">
                          <div className="w-5 h-5 border-2 border-white/30 border-t-white rounded-full animate-spin"></div>
                          Analyzing Your Face...
                        </span>
                      ) : (
                        <span className="flex items-center gap-2">
                          ✨ Get My Recommendations
                        </span>
                      )}
                    </button>
                  </div>
                </div>
              </div>
            )}

            {/* Step 3: Results */}
            {currentStep === 'results' && results && (
              <Results results={results} onStartOver={handleStartOver} />
            )}
          </>
        )}

        {/* Footer */}
        <footer className="mt-16 py-8 px-4 text-center">
          <div className="max-w-4xl mx-auto">
            <div className="flex flex-wrap justify-center gap-6 text-sm text-gray-600 mb-4">
              <div className="flex items-center gap-2">
                <span className="text-2xl">❤️</span>
                <span>Built with love for the African diaspora</span>
              </div>
              <div className="flex items-center gap-2">
                <span className="text-2xl">🚀</span>
                <span>Powered by MediaPipe & .NET</span>
              </div>
              <div className="flex items-center gap-2">
                <span className="text-2xl">🔒</span>
                <span>Your photo never leaves your device</span>
              </div>
            </div>
            <p className="text-xs text-gray-500">
              © 2026 AfriStyle Fit • Celebrating African beauty through technology
            </p>
          </div>
        </footer>
      </div>
    </div>
  );
}

export default App;