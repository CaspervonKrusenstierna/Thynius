import React from 'react'
import "./CopyRightTextContainer.css"
import { useTranslation } from 'react-i18next';

const CopyRightTextContainer = () => {
  const {t, i18n}  = useTranslation();

  return (
    <div className='CopyRightTextContainer'>© 2024 Thynius AB. {t("All Rights Reserved")}</div>
  )
}

export default CopyRightTextContainer