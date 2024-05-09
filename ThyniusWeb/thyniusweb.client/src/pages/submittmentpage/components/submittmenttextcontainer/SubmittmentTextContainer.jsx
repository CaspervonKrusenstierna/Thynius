import React, { useContext, useEffect, useRef, useState } from 'react'
import "./SubmittmentTextContainer.css"
import AnalysisSubmittmentView from './components/analysissubmittmentview/AnalysisSubmittmentView'
import RawSubmittmentView from './components/rawsubmittmentview/RawSubmittmentView'
import { submittmentInfoContext } from '../../SubmittmentPage'

const SubmittmentTextContainer = (props) => {
  const submittmentInfo = useContext(submittmentInfoContext);


  return (
    <div className='SubmittmentTextContainer'>
      {submittmentInfo.analysisActivated ? 
        <AnalysisSubmittmentView></AnalysisSubmittmentView>
        :
        <RawSubmittmentView rawText={rawText}></RawSubmittmentView>
      }
    </div>
  )
}

export default SubmittmentTextContainer