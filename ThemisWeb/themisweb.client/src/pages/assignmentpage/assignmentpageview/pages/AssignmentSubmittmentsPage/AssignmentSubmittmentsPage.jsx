import React from 'react'
import { useParams } from 'react-router-dom'
import "../../../../../shared/styles/SubmittmentsView.css"
import { SubmittmentsView } from '../../../../../shared/components/dashboard'

export const AssignmentSubmittmentsPage = () => {
  const { id } = useParams();

  return (
    <div className="mt-[120px] w-full">
        <SubmittmentsView  assignmentId={id}></SubmittmentsView>
    </div>
  )
}
export default AssignmentSubmittmentsPage