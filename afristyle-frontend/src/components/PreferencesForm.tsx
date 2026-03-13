import { HairTexture, Occasion } from '../types';

interface PreferencesFormProps {
  hairTexture: HairTexture;
  occasion: Occasion;
  forMen: boolean;
  onHairTextureChange: (texture: HairTexture) => void;
  onOccasionChange: (occasion: Occasion) => void;
  onGenderChange: (forMen: boolean) => void;
}

export const PreferencesForm: React.FC<PreferencesFormProps> = ({
  hairTexture,
  occasion,
  forMen,
  onHairTextureChange,
  onOccasionChange,
  onGenderChange,
}) => {
  return (
    <div className="space-y-6">
      {/* Hair Texture Selection */}
      <div>
        <label className="block text-sm font-semibold text-earth-dark mb-2">
          Your Hair Texture
        </label>
        <select
          value={hairTexture}
          onChange={(e) => onHairTextureChange(Number(e.target.value))}
          className="w-full px-4 py-3 rounded-lg border-2 border-kente-gold/30 focus:border-kente-orange focus:outline-none bg-white"
        >
          <option value={HairTexture.Unknown}>Select your texture...</option>
          <option value={HairTexture.CurlyLoose}>3A - Curly Loose</option>
          <option value={HairTexture.CurlyCorkscrew}>3B/3C - Curly Corkscrew</option>
          <option value={HairTexture.CoilyLoose}>4A - Coily Loose</option>
          <option value={HairTexture.CoilyZigzag}>4B - Coily Zigzag</option>
          <option value={HairTexture.CoilyTight}>4C - Coily Tight</option>
        </select>
      </div>

      {/* Occasion Selection */}
      <div>
        <label className="block text-sm font-semibold text-earth-dark mb-2">
          Occasion / Purpose
        </label>
        <select
          value={occasion}
          onChange={(e) => onOccasionChange(Number(e.target.value))}
          className="w-full px-4 py-3 rounded-lg border-2 border-kente-gold/30 focus:border-kente-orange focus:outline-none bg-white"
        >
          <option value={Occasion.Everyday}>Everyday / Casual</option>
          <option value={Occasion.Office}>Office / Professional</option>
          <option value={Occasion.Party}>Party / Night Out</option>
          <option value={Occasion.Church}>Church / Sunday Best</option>
          <option value={Occasion.Wedding}>Wedding / Special Event</option>
          <option value={Occasion.Gym}>Gym / Sports</option>
          <option value={Occasion.Travel}>Travel / Low Maintenance</option>
        </select>
      </div>

      {/* Gender Selection */}
      <div>
        <label className="block text-sm font-semibold text-earth-dark mb-2">
          Style Preference
        </label>
        <div className="flex gap-4">
          <button
            type="button"
            onClick={() => onGenderChange(false)}
            className={`flex-1 py-3 px-4 rounded-lg font-semibold transition-all ${
              !forMen
                ? 'bg-kente-orange text-white shadow-lg'
                : 'bg-gray-200 text-gray-700 hover:bg-gray-300'
            }`}
          >
            Women's Styles
          </button>
          <button
            type="button"
            onClick={() => onGenderChange(true)}
            className={`flex-1 py-3 px-4 rounded-lg font-semibold transition-all ${
              forMen
                ? 'bg-kente-orange text-white shadow-lg'
                : 'bg-gray-200 text-gray-700 hover:bg-gray-300'
            }`}
          >
            Men's Styles
          </button>
        </div>
      </div>
    </div>
  );
};