/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    colors: {
      transparent: 'transparent',
      plum: {
        light: '#fcc6dcff',
        DEFAULT: '#c27ba0ff',
        dark: '#a03a6fff',
      },
      grape: {
        light: '#e2ceff',
        DEFAULT: '#332756ff',
        dark: '',
      },
      lemon: {
        light: '#fffcf1ff',
        DEFAULT: '#fbffb9ff',
        dark: '#a4972dff',
      }
    },
    extend: {
      fontFamily: {
        sans: ['Poppins', 'sans-serif'],
      },
      gridTemplateColumns: {
        '70/30': '70% 28%'
      },
      
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}

