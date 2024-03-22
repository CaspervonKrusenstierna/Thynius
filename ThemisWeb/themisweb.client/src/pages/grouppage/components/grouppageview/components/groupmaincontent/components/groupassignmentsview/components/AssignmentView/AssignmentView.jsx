import React from 'react'
import { Link } from 'react-router-dom'
import "./AssignmentView.css"

const AssignmentView = (props) => {
  return (
  <Link className='AssignmentView' to={`/dashboard/group/${props.groupId}/Assignment/${props.assignmentId}`}>
    <img className='GroupView-Img' src={props.img}></img>
    <p className='GroupView-Text'>{props.assignmentName}</p>
  </Link>
  )
}

export default AssignmentView