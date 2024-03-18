import React from 'react'
import "./FooterMainContent.css"
import InfoRowsContainer from './components/inforowscontainer/InfoRowsContainer'

const FooterItemsTitleRowOne = "Om Themis";
const FooterItemsRowOne = [
    {ItemName: "Test1", ItemLink: "/Test1"},
    {ItemName: "Test2", ItemLink: "/Test2"},
    {ItemName: "Test3", ItemLink: "/Test3"}
]


const FooterMainContent = () => {
  return (
    <div className='FooterMainContent'>
        <InfoRowsContainer title={FooterItemsTitleRowOne} items={FooterItemsRowOne}></InfoRowsContainer>
        <div></div>
    </div>
  )
}

export default FooterMainContent