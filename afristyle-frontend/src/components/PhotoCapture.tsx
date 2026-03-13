import { useRef, useState } from 'react';

interface PhotoCaptureProps {
  onPhotoCapture: (image: HTMLImageElement) => void;
  isProcessing: boolean;
}

export const PhotoCapture: React.FC<PhotoCaptureProps> = ({ onPhotoCapture, isProcessing }) => {
  const fileInputRef = useRef<HTMLInputElement>(null);
  const [preview, setPreview] = useState<string | null>(null);

  const handleFileSelect = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (!file) return;

    // Validate file type
    if (!file.type.startsWith('image/')) {
      alert('Please select an image file');
      return;
    }

    // Validate file size (max 10MB)
    if (file.size > 10 * 1024 * 1024) {
      alert('Image too large. Please use an image under 10MB.');
      return;
    }

    // Read file and create image element
    const reader = new FileReader();
    reader.onload = (e) => {
      const imageUrl = e.target?.result as string;
      setPreview(imageUrl);

      // Create image element for MediaPipe
      const img = new Image();
      img.onload = () => {
        onPhotoCapture(img);
      };
      img.src = imageUrl;
    };
    reader.readAsDataURL(file);
  };

  const handleButtonClick = () => {
    fileInputRef.current?.click();
  };

  return (
    <div className="w-full max-w-md mx-auto">
      {/* Photo Preview */}
      {preview && (
        <div className="mb-6 rounded-lg overflow-hidden shadow-lg">
          <img src={preview} alt="Selected photo" className="w-full h-auto" />
        </div>
      )}

      {/* Upload Button */}
      <input
        ref={fileInputRef}
        type="file"
        accept="image/*"
        onChange={handleFileSelect}
        className="hidden"
      />

      <button
        onClick={handleButtonClick}
        disabled={isProcessing}
        className={`
          w-full py-4 px-6 rounded-lg font-semibold text-white text-lg
          transition-all duration-200 transform
          ${
            isProcessing
              ? 'bg-gray-400 cursor-not-allowed'
              : 'bg-kente-orange hover:bg-kente-red hover:scale-105 active:scale-95 shadow-lg'
          }
        `}
      >
        {isProcessing ? (
          <span className="flex items-center justify-center gap-2">
            <svg className="animate-spin h-5 w-5" viewBox="0 0 24 24">
              <circle
                className="opacity-25"
                cx="12"
                cy="12"
                r="10"
                stroke="currentColor"
                strokeWidth="4"
                fill="none"
              />
              <path
                className="opacity-75"
                fill="currentColor"
                d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"
              />
            </svg>
            Analyzing...
          </span>
        ) : preview ? (
          'Choose Different Photo'
        ) : (
          '📸 Upload Your Photo'
        )}
      </button>

      {/* Instructions */}
      <div className="mt-4 p-4 bg-kente-gold/10 rounded-lg">
        <p className="text-sm text-earth-dark">
          <strong>Tips for best results:</strong>
        </p>
        <ul className="text-sm text-earth-dark mt-2 space-y-1 list-disc list-inside">
          <li>Use a clear, front-facing photo</li>
          <li>Good lighting with no shadows</li>
          <li>Hair pulled back or tied up</li>
          <li>Neutral expression</li>
        </ul>
      </div>
    </div>
  );
};