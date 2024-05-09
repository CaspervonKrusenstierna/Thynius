import React, { createContext, useEffect, useRef, useState } from 'react'
import { DashboardHeader } from '../../shared/components/dashboard'
import SubmittmentSideMenu from './components/submittmentsidemenu/SubmittmentSideMenu'
import SubmittmentTextContainer from './components/submittmenttextcontainer/SubmittmentTextContainer'
import { useParams } from "react-router-dom"
import useSubmittmentInfo from './hooks/useSubmittmentInfo'
import "./SubmittmentPage.css"
import AssureLoggedIn from '../../shared/hooks/useAssureLoggedIn'
import SubmittmentPageSideBar from './submittmentpagesidebar/SubmittmentPageSideBar'
import SubmittmentAnalysisView from './views/submittmentanalysisview/SubmittmentAnalysisView'
import SubmittmentRawView from './views/submittmentrawview/SubmittmentRawView'

export const submittmentInfoContext = createContext();

const SubmittmentView = () => {
  AssureLoggedIn();
  const [analysisActivated, setAnalysisActivated] = useState(false);
  const currIndex = useRef(-1);
  const [indexIsTouched, setIndexIsTouched] = useState(false);
  const { id } = useParams();
  const submittmentInfo = useSubmittmentInfo(id);

  return (
    <submittmentInfoContext.Provider value={{links: submittmentInfo, currIndex: currIndex, indexIsTouched: indexIsTouched, setIndexIsTouched: setIndexIsTouched}}>
      <DashboardHeader></DashboardHeader>
      <div className='SubmittmentPage'>
        <SubmittmentPageSideBar onAnalysisActivate={() => {setAnalysisActivated(true)}}></SubmittmentPageSideBar>
        {analysisActivated ? <SubmittmentAnalysisView/> : <SubmittmentRawView rawTextURL={submittmentInfo.rawTextURL}/>}
      </div>
    </submittmentInfoContext.Provider>
  )
}

export default SubmittmentView