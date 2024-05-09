import React, { useContext } from 'react'
import "./AssignmentOverviewPage.css"
import { assignmentInfoContext } from '../../AssignmentPageView'

const AssignmentOverviewPage = (props) => {
  const assignmentInfo = useContext(assignmentInfoContext);
  return (
    <div className='AssignmentOverviewPage'><p className='AssignmentOverviewPage-Text'>{assignmentInfo?.Description}</p></div>
  )
}

export default AssignmentOverviewPage