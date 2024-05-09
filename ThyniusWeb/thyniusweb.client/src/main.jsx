import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import translationsvSE from "./languages/sv-SE.json"
import detector from 'i18next-browser-languagedetector';

const options = {
    order: ['querystring', 'navigator'],
    lookupQuerystring: 'lng'
  }

i18n.use(detector).use(initReactI18next).init({
    detection: options,
    resources: {
      sv:  translationsvSE
    },
    fallbackLng: "en",

    interpolation: {
      escapeValue: false
    }
  });

ReactDOM.createRoot(document.getElementById('root')).render(
    <App />
);