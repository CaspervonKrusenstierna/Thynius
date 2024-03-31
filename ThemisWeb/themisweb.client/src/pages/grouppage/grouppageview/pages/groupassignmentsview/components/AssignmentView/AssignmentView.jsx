import React, { useContext } from 'react'
import { Link } from 'react-router-dom'
import "./AssignmentView.css"
import { ImageAvatar } from '../../../../../../../shared/assets'
import { groupinfoContext } from '../../../../GroupPageView'

const AssignmentView = (props) => {
  const groupInfo = useContext(groupinfoContext);
  return (
  <Link className='AssignmentView' state={{userDatas: groupInfo.userDatas, assignmentName: props.assignmentName}} to={`/dashboard/Assignment/${props.assignmentId}/general`}>
    <img className='AssignmentView-Img' src={/*props.img*/ ImageAvatar}></img>
    <p className='AssignmentView-Text'>{props.assignmentName}</p>
  </Link>
  )
}

export default AssignmentView