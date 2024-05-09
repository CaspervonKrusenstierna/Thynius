import React from 'react'
import "./SubmittmentPageSideBar.css"
import { useTranslation } from 'react-i18next';

const SubmittmentPageSideBar = (props) => {
  const submittmentInfo = useContext(submittmentInfoContext);
  const detectionInfo = useDetectionInfo(submittmentInfo.links?.detectionDataURL)
  return (
    <div className='SubmittmentSideMenu'>
    {submittmentInfo.analysisActivated ? 
      <div className='flex flex-col mt-3 w-full'>
        <p className='ml-[5%] pb-3 pt-3 font-bold text-xl'>{t("Analysis")}</p>

        <p className='ml-[5%] pb-3 pt-3 text-lg font-bold'>{t("Remarks")}</p>
        {detectionInfo?.remarks.map(s => <SubmittmentRemark remark={s}></SubmittmentRemark>)}

        <p className='ml-[5%] pb-3 pt-3 text-lg font-bold'>{t("Statistics")}</p>
        {detectionInfo?.statistics.map(s => <InputsDataStat info={s.info} infoType={s.infoType}></InputsDataStat>)}

        <p className=' text-lg ml-[5%] pt-3 font-bold'>{t("Points of interest")}</p>
        <div className='max-h-[200px] md:max-h-[600px] overflow-scroll mb-3 ml-[5%] pt-3 overflow-x-hidden'>
          {detectionInfo?.momentsOfInterest.map(s => <InterestMoment onClick={(index) => {submittmentInfo.currIndex.current = index; submittmentInfo.setIndexIsTouched(!submittmentInfo.indexIsTouched)}} index={s.Index} reasonOfInterest={s.reason}></InterestMoment>)}
        </div>
        <InputIndexWidget/>
      
      </div> : 
      <Button className="mt-3 text-black" onClick={() => {submittmentInfo.setAnalysisActivated(true)}}>{t("Activate analysis")}</Button>
    }
    </div>
   )
}

export default SubmittmentPageSideBar