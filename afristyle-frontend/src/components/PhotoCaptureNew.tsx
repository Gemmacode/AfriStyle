import { useRef, useState } from 'react';

interface PhotoCaptureProps {
  onPhotoCapture: (image: HTMLImageElement) => void;
  isProcessing: boolean;
}

export const PhotoCapture: React.FC<PhotoCaptureProps> = ({ onPhotoCapture, isProcessing }) => {
  const fileInputRef = useRef<HTMLInputElement>(null);
  const [preview, setPreview] = useState<string | null>(null);
  const [isDragOver, setIsDragOver] = useState(false);

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

  const handleDragOver = (e: React.DragEvent) => {
    e.preventDefault();
    setIsDragOver(true);
  };

  const handleDragLeave = (e: React.DragEvent) => {
    e.preventDefault();
    setIsDragOver(false);
  };

  const handleDrop = (e: React.DragEvent) => {
    e.preventDefault();
    setIsDragOver(false);

    const files = e.dataTransfer.files;
    if (files.length > 0) {
      const file = files[0];
      if (file.type.startsWith('image/')) {
        // Create a fake event to reuse the existing logic
        const fakeEvent = {
          target: { files: [file] }
        } as React.ChangeEvent<HTMLInputElement>;
        handleFileSelect(fakeEvent);
      }
    }
  };

  const handleButtonClick = () => {
    fileInputRef.current?.click();
  };

  return (
    <div className="w-full max-w-md mx-auto">
      {/* Photo Preview */}
      {preview && (
        <div className="mb-8 rounded-2xl overflow-hidden shadow-2xl border-4 border-kente-gold/30 transform hover:scale-105 transition-transform duration-300">
          <img src={preview} alt="Selected photo" className="w-full h-auto" />
          <div className="bg-gradient-to-t from-black/50 to-transparent p-4">
            <p className="text-white text-center font-medium">✨ Perfect! Your photo looks great</p>
          </div>
        </div>
      )}

      {/* Upload Area */}
      {!preview && (
        <div
          className={`
            relative border-3 border-dashed rounded-3xl p-8 text-center cursor-pointer
            transition-all duration-300 transform hover:scale-105
            ${isDragOver
              ? 'border-kente-orange bg-kente-gold/10 shadow-2xl'
              : 'border-kente-gold/50 bg-white/50 hover:bg-kente-gold/5 hover:border-kente-orange hover:shadow-xl'
            }
          `}
          onDragOver={handleDragOver}
          onDragLeave={handleDragLeave}
          onDrop={handleDrop}
          onClick={handleButtonClick}
        >
          {/* Animated background */}
          <div className="absolute inset-0 rounded-3xl bg-gradient-to-br from-kente-gold/5 to-kente-orange/5 opacity-0 hover:opacity-100 transition-opacity duration-300"></div>

          <div className="relative z-10">
            {/* Icon */}
            <div className="w-20 h-20 mx-auto mb-6 bg-gradient-to-br from-kente-gold to-kente-orange rounded-full flex items-center justify-center shadow-lg">
              <span className="text-4xl text-white">📸</span>
            </div>

            {/* Text */}
            <h3 className="text-2xl font-bold text-earth-dark mb-3">
              {isDragOver ? 'Drop your photo here!' : 'Upload Your Photo'}
            </h3>
            <p className="text-gray-600 mb-6 text-lg">
              {isDragOver
                ? 'Release to upload ✨'
                : 'Drag & drop or click to browse'
              }
            </p>

            {/* Upload Button */}
            <button
              className="px-8 py-4 bg-gradient-to-r from-kente-orange to-kente-red text-white rounded-2xl font-semibold text-lg shadow-lg hover:shadow-xl transform hover:scale-105 active:scale-95 transition-all duration-200"
            >
              Choose Photo
            </button>
          </div>

          {/* Floating elements */}
          <div className="absolute -top-4 -right-4 w-8 h-8 bg-kente-gold rounded-full animate-bounce" style={{animationDelay: '0s'}}></div>
          <div className="absolute -bottom-4 -left-4 w-6 h-6 bg-kente-orange rounded-full animate-bounce" style={{animationDelay: '0.5s'}}></div>
          <div className="absolute top-1/2 -right-6 w-4 h-4 bg-kente-red rounded-full animate-bounce" style={{animationDelay: '1s'}}></div>
        </div>
      )}

      {/* Hidden File Input */}
      <input
        ref={fileInputRef}
        type="file"
        accept="image/*"
        onChange={handleFileSelect}
        className="hidden"
      />

      {/* Instructions */}
      <div className="mt-8 p-6 bg-gradient-to-r from-kente-gold/10 to-kente-orange/10 rounded-2xl border border-kente-gold/20">
        <div className="flex items-center gap-3 mb-4">
          <div className="w-10 h-10 bg-kente-gold rounded-full flex items-center justify-center">
            <span className="text-white text-xl">💡</span>
          </div>
          <h4 className="text-lg font-semibold text-earth-dark">Tips for best results</h4>
        </div>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4 text-sm text-earth-dark">
          <div className="flex items-center gap-2">
            <span className="text-kente-orange">📱</span>
            <span>Use a clear, front-facing photo</span>
          </div>
          <div className="flex items-center gap-2">
            <span className="text-kente-orange">💡</span>
            <span>Good lighting with no shadows</span>
          </div>
          <div className="flex items-center gap-2">
            <span className="text-kente-orange">👩</span>
            <span>Hair pulled back or tied up</span>
          </div>
          <div className="flex items-center gap-2">
            <span className="text-kente-orange">😊</span>
            <span>Neutral expression</span>
          </div>
        </div>
      </div>
    </div>
  );
};