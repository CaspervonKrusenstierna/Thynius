import React from 'react'
import "./AssignmentSubmittmentsPage.css"
import SubmittmentsView from '../../../../../shared/components/submittmentsview/SubmittmentsView'
import { useParams } from 'react-router-dom'
import useAssignmentSubmittments from './hooks/useAssignmentSubmittments'

submittments=[{UserName: "Casper1", WarningLevel: 1}, {UserName: "Casper1", WarningLevel: 0}, {UserName: "Casper1", WarningLevel: 1}, {UserName: "Casper1", WarningLevel: 1}, {UserName: "Casper1", WarningLevel: 2}, {UserName: "Casper1", WarningLevel: 0}, {UserName: "Casper1", WarningLevel: 2}]
export const AssignmentSubmittmentsPage = () => {
  const { id } = useParams();

  return (
    <div className='AssignmentSubmittmentsPage'>
        <SubmittmentsView submittments={/*useAssignmentSubmittments(id)*/submittments}></SubmittmentsView>
    </div>
  )
}
export default AssignmentSubmittmentsPage