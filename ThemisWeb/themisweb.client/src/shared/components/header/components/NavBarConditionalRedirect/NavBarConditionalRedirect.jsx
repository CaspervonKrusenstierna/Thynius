import React from 'react'
import "./NavBarConditionalRedirect.css"

const NavBarConditionalRedirect = (props) => {
  return (
    <button className='NavBarConditionalRedirect' onClick={props.onClick}>{props.text}</button>
  )
}

export default NavBarConditionalRedirect