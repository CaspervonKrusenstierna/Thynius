import React, { createContext, useEffect, useRef, useState } from 'react'
import { DashboardHeader } from '../../shared/components/dashboard'
import SubmittmentSideMenu from './components/submittmentsidemenu/SubmittmentSideMenu'
import SubmittmentTextContainer from './components/submittmenttextcontainer/SubmittmentTextContainer'
import { useParams } from "react-router-dom"
import useSubmittmentInfo from './hooks/useSubmittmentInfo'

export const submittmentInfoContext = createContext();

const SubmittmentView = () => {
  const [analysisActivated, setAnalysisActivated] = useState(false);
  const currIndex = useRef(-1);
  const [indexIsTouched, setIndexIsTouched] = useState(false);
  const { id } = useParams();
  const submittmentInfo = useSubmittmentInfo(id);
  return (
    <submittmentInfoContext.Provider value={{links: submittmentInfo, analysisActivated: analysisActivated, setAnalysisActivated: setAnalysisActivated, currIndex: currIndex, indexIsTouched: indexIsTouched, setIndexIsTouched: setIndexIsTouched}}>
      <DashboardHeader></DashboardHeader>
      <div className='flex flex-row justify-center w-[100vw] mt-[70px] h-[800px] mb-1'>
        <SubmittmentSideMenu></SubmittmentSideMenu>
        <SubmittmentTextContainer></SubmittmentTextContainer>
      </div>
    </submittmentInfoContext.Provider>
  )
}

export default SubmittmentView