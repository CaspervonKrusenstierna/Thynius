import React, { createContext } from 'react'
import "../../../shared/styles/DashboardContainer.css"
import useSubmittmentInfo from './hooks/useSubmittmentInfo';
import { useParams } from 'react-router-dom';

export const submittmentInfoContext = createContext();

const SubmittmentPageView = () => {
  let { id } = useParams();
  let submittmentInfo = useSubmittmentInfo(id);

  return (
    <submittmentInfoContext.Provider value={submittmentInfo}>
      <div className='DashboardContainer-Container'>
        <div className='DashboardContainer'>
        </div>
      </div>
    </submittmentInfoContext.Provider>
  )
}

export default SubmittmentPageView