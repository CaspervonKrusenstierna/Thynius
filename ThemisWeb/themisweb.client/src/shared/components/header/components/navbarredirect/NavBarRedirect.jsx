import React from 'react'
import "./NavBarRedirect.css"
import { Link } from 'react-router-dom'

const NavBarRedirect = (props) => {
  
  return (
      <a className='NavBarRedirect' href={props.href}>{props.redirect}</a>
  )
}

export default NavBarRedirect