import React from 'react'
import { Link } from 'react-router-dom'
import "./NavBarButtonRedirect.css"
import UserIcon from './usericon.png'

const NavBarButtonRedirect = (props) => {
  return (
      <button onClick={props.onClick} className='NavBarButtonRedirect'>
          <img className="NavBarButtonRedirect-png" src={UserIcon}></img>
          <p className='NavBarButtonRedirect-text'>{props.text}</p>
      </button>
  )
}

export default NavBarButtonRedirect