import React from 'react'
import "./TosContainerItem.css"
import { Link } from 'react-router-dom'
const TosContainerItem = (props) => {
  return (
    <Link onClick={() => {window.scrollTo({top: 0, left: 0, behavior: 'instant'})}} className='TosContainerItem' to={props.link}>{props.text}</Link>
  )
}

export default TosContainerItem