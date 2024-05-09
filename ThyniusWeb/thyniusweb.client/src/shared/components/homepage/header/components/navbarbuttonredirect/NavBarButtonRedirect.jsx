import React from 'react'
import { Link } from 'react-router-dom'
import "./NavBarButtonRedirect.css"
import UserIcon from './usericon.png'

const NavBarButtonRedirect = (props) => {
  return (
      <Link to={props.link} onClick={() => {window.scrollTo({top: 0, left: 0, behavior: 'instant'});}} className='NavBarButtonRedirect'>
          <img alt='' className="NavBarButtonRedirect-png" src={UserIcon}></img>
          <p className='NavBarButtonRedirect-text'>{props.text}</p>
      </Link>
  )
}

export default NavBarButtonRedirect