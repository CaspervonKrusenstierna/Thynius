import React from 'react'
import useNotSubmittedUsers from './hooks/useNotSubmittedUsers'
import { useParams } from 'react-router-dom'
import NonSubmittmentsView from './nonsubmittmentsview/NonSubmittmentsView';

const AssignmentNotSubmittedPage = () => {
  const { id } = useParams();
  const users = useNotSubmittedUsers(id);
  return (
    <div className='mt-[120px] w-full'>
      <NonSubmittmentsView assignmentId={id}></NonSubmittmentsView>
    </div>
  )
}

export default AssignmentNotSubmittedPage