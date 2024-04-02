import React from 'react'
import { Link } from 'react-router-dom'

const DUCLinkDropdownItem = (props) => {
  return (
    <Link onClick={props.onClick ? props.onClick : () => {}} className='DUCDropdownItem' to={props.link}>{props.text}</Link>
  )
}

export default DUCLinkDropdownItem