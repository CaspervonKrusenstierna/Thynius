import React from 'react'
import useNotSubmittedUsers from './hooks/useNotSubmittedUsers'
import { useParams } from 'react-router-dom'

const AssignmentNotSubmittedPage = () => {
  const { id } = useParams();
  const users = useNotSubmittedUsers(id);
  return (
    <div className='mt-[120px]'>
        <div className='SubmittmentsHeader'>
            <p className='SubmittmentsHeader-Title'>Ej Inlämnade</p>
        </div>
        <div className='SubmittmentsContainer'>{users?.map(i => <div className='Submittment'>{i.UserName}<p className="text-red-400 mr-3 text-base shadow-sm">Inlämning saknas</p></div>)}</div>
    </div>
  )
}

export default AssignmentNotSubmittedPage