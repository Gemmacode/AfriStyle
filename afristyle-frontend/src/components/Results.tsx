import type { RecommendationResponse } from '../types';

interface ResultsProps {
  results: RecommendationResponse;
  onStartOver: () => void;
}

export const Results: React.FC<ResultsProps> = ({ results, onStartOver }) => {
  return (
    <div className="w-full max-w-6xl mx-auto">
      {/* Face Shape Analysis */}
      <div className="bg-white rounded-lg shadow-xl p-8 mb-8">
        <h2 className="text-3xl font-bold text-kente-brown mb-4">
          Your Face Shape: {results.faceShapeName}
        </h2>
        <div className="flex items-center gap-2 mb-4">
          <div className="text-sm text-gray-600">
            Confidence: {Math.round(results.confidence * 100)}%
          </div>
        </div>
        <p className="text-gray-700 mb-4">{results.faceShapeDescription}</p>
        <div className="bg-kente-gold/10 p-4 rounded-lg">
          <p className="text-sm font-semibold text-earth-dark">💡 AfriStyle Tip:</p>
          <p className="text-sm text-earth-dark mt-1">{results.faceShapeTip}</p>
        </div>
      </div>

      {/* Recommendations Grid */}
      <h3 className="text-2xl font-bold text-kente-brown mb-6">
        Top Recommendations for You
      </h3>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 mb-8">
        {results.recommendations.map((style) => (
          <div
            key={style.id}
            className="bg-white rounded-lg shadow-lg overflow-hidden hover:shadow-2xl transition-shadow"
          >
            {/* Placeholder for image */}
            <div className="h-64 bg-gradient-to-br from-kente-gold/20 to-kente-orange/20 flex items-center justify-center">
              <span className="text-6xl">💇🏾‍♀️</span>
            </div>

            <div className="p-6">
              {/* Fit Score Badge */}
              <div className="flex items-center justify-between mb-3">
                <span
                  className={`px-3 py-1 rounded-full text-sm font-bold ${
                    style.fitScore >= 90
                      ? 'bg-green-100 text-green-800'
                      : style.fitScore >= 75
                      ? 'bg-blue-100 text-blue-800'
                      : 'bg-yellow-100 text-yellow-800'
                  }`}
                >
                  {style.fitScore}% {style.fitLabel}
                </span>
                {style.isProtectiveStyle && (
                  <span className="text-xs bg-purple-100 text-purple-800 px-2 py-1 rounded-full">
                    Protective
                  </span>
                )}
              </div>

              <h4 className="text-xl font-bold text-earth-dark mb-2">{style.name}</h4>
              <p className="text-sm text-gray-600 mb-4">{style.description}</p>

              {/* Score Breakdown */}
              <div className="space-y-2 text-xs">
                <div className="flex justify-between">
                  <span className="text-gray-600">Face Shape Fit:</span>
                  <span className="font-semibold">{style.faceShapeScore}%</span>
                </div>
                <div className="flex justify-between">
                  <span className="text-gray-600">Texture Match:</span>
                  <span className="font-semibold">{style.textureScore}%</span>
                </div>
                <div className="flex justify-between">
                  <span className="text-gray-600">Occasion:</span>
                  <span className="font-semibold">{style.occasionScore}%</span>
                </div>
                <div className="flex justify-between">
                  <span className="text-gray-600">Maintenance:</span>
                  <span className="font-semibold">{style.maintenanceLevel}</span>
                </div>
              </div>
            </div>
          </div>
        ))}
      </div>

      {/* Start Over Button */}
      <div className="text-center">
        <button
          onClick={onStartOver}
          className="px-8 py-3 bg-kente-brown text-white rounded-lg font-semibold hover:bg-earth-dark transition-colors"
        >
          ← Try Another Photo
        </button>
      </div>
    </div>
  );
};