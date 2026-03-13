/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        // Kente-inspired color palette
        'kente-gold': '#D4A574',
        'kente-brown': '#8B4513',
        'kente-orange': '#E07B39',
        'kente-red': '#C1440E',
        'earth-dark': '#3E2723',
        'earth-light': '#F5E6D3',
      },
    },
  },
  plugins: [],
}