import React from 'react'
import "./TextsHeader.css"
import { useTranslation } from 'react-i18next'

const TextsHeader = () => {
  const [t, i18n] = useTranslation();
  return (
    <div className='TextsHeader'>{t("Texts")}</div>
  )
}

export default TextsHeader