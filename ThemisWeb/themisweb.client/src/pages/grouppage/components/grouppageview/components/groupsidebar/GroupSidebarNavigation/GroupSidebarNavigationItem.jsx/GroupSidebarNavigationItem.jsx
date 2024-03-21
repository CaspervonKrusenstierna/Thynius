import React from 'react'
import { Link } from 'react-router-dom'
import "./GroupSidebarNavigationItem.css"

const GroupSidebarNavigationItem = (props) => {
  return (
    <Link to={props.link} className='GroupSidebarNavigationItem'>
        <p className='GroupSidebarNavigationItem-Text'>{props.Text}</p>
    </Link>
  )
}

export default GroupSidebarNavigationItem