import React, { useContext, useEffect, useState } from 'react'
import "./SubmittmentTextContainer.css"
import AnalysisSubmittmentView from './components/analysissubmittmentview/AnalysisSubmittmentView'
import RawSubmittmentView from './components/rawsubmittmentview/RawSubmittmentView'
import { submittmentInfoContext } from '../../SubmittmentPage'

const SubmittmentTextContainer = (props) => {
  const submittmentInfo = useContext(submittmentInfoContext);
  const [rawText, setRawText] = useState();

  useEffect(() => {
    async function getText(){
      await fetch(submittmentInfo.links.rawTextURL).then(s => s.text()).then(s => setRawText(s));
    }
    getText();
  }, [submittmentInfo.links])

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