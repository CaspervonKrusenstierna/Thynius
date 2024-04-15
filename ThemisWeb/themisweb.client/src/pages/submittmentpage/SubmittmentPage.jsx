import React, { createContext, useEffect, useRef, useState } from 'react'
import { DashboardHeader } from '../../shared/components/dashboard'
import SubmittmentSideMenu from './components/submittmentsidemenu/SubmittmentSideMenu'
import SubmittmentTextContainer from './components/submittmenttextcontainer/SubmittmentTextContainer'
import { useParams } from "react-router-dom"
import useSubmittmentInfo from './hooks/useSubmittmentInfo'
import "./SubmittmentPage.css"
import AssureLoggedIn from '../../shared/hooks/useAssureLoggedIn'

export const submittmentInfoContext = createContext();

const SubmittmentView = () => {
  AssureLoggedIn();
  const [analysisActivated, setAnalysisActivated] = useState(false);
  const currIndex = useRef(-1);
  const [indexIsTouched, setIndexIsTouched] = useState(false);
  const { id } = useParams();
  const submittmentInfo = useSubmittmentInfo(id);
  return (
    <submittmentInfoContext.Provider value={{links: submittmentInfo, analysisActivated: analysisActivated, setAnalysisActivated: setAnalysisActivated, currIndex: currIndex, indexIsTouched: indexIsTouched, setIndexIsTouched: setIndexIsTouched}}>
      <DashboardHeader></DashboardHeader>
      <div className='SubmittmentPage'>
        <SubmittmentSideMenu></SubmittmentSideMenu>
        <SubmittmentTextContainer></SubmittmentTextContainer>
      </div>
    </submittmentInfoContext.Provider>
  )
}

export default SubmittmentView