import React from 'react'
import "./GroupView.css"
import { Link } from 'react-router-dom'

const GroupView = (props) => {
  return (
    <Link className='GroupView' to={"/dashboard/group/" + props.groupId + "/Home"}>
      <img className='GroupView-Img' src={props.img}></img>
      <p className='GroupView-Text'>{props.name}</p>
    </Link>
  )
}

export default GroupView