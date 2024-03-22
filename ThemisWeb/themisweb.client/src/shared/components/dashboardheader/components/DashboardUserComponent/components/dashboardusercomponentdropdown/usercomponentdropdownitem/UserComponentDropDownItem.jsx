import React from 'react'
import { Link } from 'react-router-dom'
import "./UserComponentDropDownItem.css"

const UserComponentDropDownItem = (props) => {
  return (
    <Link onClick={props.onClick ? props.onClick : () => {}} className='UserComponentDropDownItem' to={props.link}>{props.text}</Link>
  )
}

export default UserComponentDropDownItem