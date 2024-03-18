import React from 'react'
import "./DashboardNavItem.css"
import { Link } from 'react-router-dom'

const DashboardNavItem = (props) => {
  return (
    <Link className='DashboardNavItem' to={props.link}>
        <img className="DashboardNavItem-Img" src={props.img}></img>
        <p className='DashboardNavItem-Text'>{props.title}</p>
    </Link>
  )
}

export default DashboardNavItem