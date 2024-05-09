import React from 'react'
import "./MainContentContainer.css"
import { useTranslation } from 'react-i18next'
const MainContentContainer = () => {
  const {t, i18n}  = useTranslation();
  return (
    <div className='MainContentContainer'>
      <div className='flex flex-row justify-start w-full h-[600px]'>
        <p className='mt-[150px] text-[48px] leading-[56px] font-bold'>{t("Create a Fair School")}<br/> <span className='text-[var(--themeMainColor)]'>{t("For Everyone.")}</span></p>
      </div>

      <div className='flex flex-row justify-start w-[93%] min-h-[600px]'>
        <div className='flex flex-col mt-[120px] gap-5 '>
          <p className='text-[30px] leading-[34px] md:text-[40px] font-bold md:leading-[44px] '>{t("A new way of")} <br/>{t("preventing cheating.")}</p>
          <p className='text-[18px] leading-[22px] md:text-[24px] md:leading-[30px]'>{t("Thynius thinks outside the text.")}<br/>{t("Instead of analyzing the text when it's already written,")}<br/>{t("Thynius does its analyzation during the writing.")} <br/>{t("This creates opportunities never seen before.")}</p>
        </div>
        
      </div>
    </div>
  )
}

export default MainContentContainer