import React from 'react'
import { useParams } from 'react-router-dom'
import NonSubmittmentsView from './nonsubmittmentsview/NonSubmittmentsView';

const AssignmentNotSubmittedPage = () => {
  const { id } = useParams();
  return (
    <div className='mt-[120px] w-full'>
      <NonSubmittmentsView assignmentId={id}></NonSubmittmentsView>
    </div>
  )
}

export default AssignmentNotSubmittedPage