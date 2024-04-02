import React from 'react'
import "./TosContainerItem.css"
import { Link } from 'react-router-dom'
const TosContainerItem = (props) => {
  return (
    <Link className='TosContainerItem' to={props.link}>{props.text}</Link>
  )
}

export default TosContainerItem