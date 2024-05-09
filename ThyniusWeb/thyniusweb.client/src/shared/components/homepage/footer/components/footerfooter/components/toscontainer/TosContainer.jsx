import React from 'react'
import "./TosContainer.css"
import TosContainerItem from './components/toscontaineritem/TosContainerItem'
import { useTranslation } from 'react-i18next';

const TosContainer = () => {
  const {t, i18n}  = useTranslation();

  return (
    <div className='TosContainer'>
        <TosContainerItem text={t("Privacy policy")} link="/privacy-policy"></TosContainerItem>
        <TosContainerItem text="Cookie Policy" link="/cookie-policy"></TosContainerItem>
    </div>
  )
}

export default TosContainer