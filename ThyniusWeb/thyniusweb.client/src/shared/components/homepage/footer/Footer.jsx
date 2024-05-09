import React from 'react'
import "./Footer.css"
import Wave from "./Wave.svg"
import FooterMainContent from './components/footermaincontent/FooterMainContent';
import FooterFooter from './components/footerfooter/FooterFooter';
const Footer = () => {
  return (

    <div className='Footer'>
      <img alt='' className="Footer-Wave-Img" src={Wave}></img>
      <FooterMainContent></FooterMainContent>
      <FooterFooter></FooterFooter>
    </div>
  )
}

export default Footer