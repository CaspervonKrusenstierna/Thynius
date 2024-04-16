import React from 'react'
import "./FooterMainContent.css"
import InfoRowsContainer from './components/inforowscontainer/InfoRowsContainer'

const FooterItemsTitleRowOne = "Om Thynius";
const FooterItemsRowOne = [
    {ItemName: "Om oss", ItemLink: "/about"},
]
const FooterItemsTitleRowTwo = "Hjälp";
const FooterItemsRowTwo = [
    {ItemName: "Kontakta oss", ItemLink: "/contact"},
]



const FooterMainContent = () => {
  return (
    <div className='FooterMainContent'>
        <InfoRowsContainer title={FooterItemsTitleRowOne} items={FooterItemsRowOne}></InfoRowsContainer>
        <InfoRowsContainer title={FooterItemsTitleRowTwo} items={FooterItemsRowTwo}></InfoRowsContainer>
        <div></div>
    </div>
  )
}

export default FooterMainContent