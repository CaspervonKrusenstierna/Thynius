import React, { useContext } from 'react'
import "./SubmittmentSideMenu.css"
import { Button } from "@/components/ui/button"
import { submittmentInfoContext } from '../../SubmittmentPage';
import useDetectionInfo from './hooks/useDetectionInfo';
import InterestMoment from './components/interestmoment/InterestMoment';
import InputsDataStat from './components/inputsdatastat/InputsDataStat';
const SubmittmentSideMenu = (props) => {
  const submittmentInfo = useContext(submittmentInfoContext);
  const detectionInfo = useDetectionInfo(submittmentInfo.links?.detectionDataURL)
  return (
    <div className='SubmittmentSideMenu'>
    {submittmentInfo.analysisActivated ? 
      <div className='flex flex-col mt-3 w-full'>
        <p className='ml-[5%] pb-3 pt-3 font-bold text-xl'>Analys</p>
        <p className='ml-[5%] pb-3 pt-3 text-lg'>Statistik</p>
        {detectionInfo?.statistics.map(s => <InputsDataStat info={s.info} infoType={s.infoType}></InputsDataStat>)}
        <p className='ml-[5%] pb-3 pt-3 text-lg'>Stunder av intresse</p>
        {detectionInfo?.momentsOfInterest.map(s => <InterestMoment onClick={(index) => {submittmentInfo.currIndex.current = index; submittmentInfo.setIndexIsTouched(!submittmentInfo.indexIsTouched)}} index={s.index} reasonOfInterest={s.reasonOfInterest}></InterestMoment>)}
      </div> : 
      <Button className="mt-3 text-black" onClick={() => {submittmentInfo.setAnalysisActivated(true)}}>Aktivera analysläge</Button>
    }
    </div>
   )
}

export default SubmittmentSideMenu