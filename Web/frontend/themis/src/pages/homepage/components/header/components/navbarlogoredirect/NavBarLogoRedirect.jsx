import React from 'react'
import "./NavBarLogoRedirect.css"
import { Link } from 'react-router-dom'

const NavBarLogoRedirect = () => {
  return (
    <a href="/" style={{textDecoration: "none", zIndex: 9}}><a className='NavBarLogoRedirect'>Themis</a></a>
  )
}

export default NavBarLogoRedirect