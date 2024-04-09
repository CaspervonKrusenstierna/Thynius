import React, { useContext } from 'react'
import "./SubmittmentSideMenu.css"
import { Button } from "@/components/ui/button"
import { submittmentInfoContext } from '../../SubmittmentPage';
import useDetectionInfo from './hooks/useDetectionInfo';
import InterestMoment from './components/interestmoment/InterestMoment';
const SubmittmentSideMenu = (props) => {
  const submittmentInfo = useContext(submittmentInfoContext);
  const detectionInfo = useDetectionInfo(submittmentInfo.links?.detectionDataURL)
  return (
    <div className='SubmittmentSideMenu'>
    {submittmentInfo.analysisActivated ? 
      <div className='mt-3'>
        {detectionInfo?.momentsOfInterest.map(s => {<InterestMoment info={s}></InterestMoment>})}
      </div> : 
      <Button className="mt-3 text-black" onClick={() => {submittmentInfo.setAnalysisActivated(true)}}>Aktivera analysläge</Button>
    }
    </div>
   )
}

export default SubmittmentSideMenu