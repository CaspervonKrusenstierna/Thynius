import React from 'react'
import "./FooterMainContent.css"
import InfoRowsContainer from './components/inforowscontainer/InfoRowsContainer'
import { useTranslation } from 'react-i18next';




const FooterMainContent = () => {
  const {t, i18n}  = useTranslation();

  const FooterItemsTitleRowOne = t("About Thymius");
  const FooterItemsRowOne = [
      {ItemName: t("About us"), ItemLink: "/about"},
  ]
  const FooterItemsTitleRowTwo = t("Help");
  const FooterItemsRowTwo = [
      {ItemName:  t("Contact us"), ItemLink: "/contact"},
  ]
  return (
    <div className='FooterMainContent'>
        <InfoRowsContainer title={FooterItemsTitleRowOne} items={FooterItemsRowOne}></InfoRowsContainer>
        <InfoRowsContainer title={FooterItemsTitleRowTwo} items={FooterItemsRowTwo}></InfoRowsContainer>
        <div></div>
    </div>
  )
}

export default FooterMainContent