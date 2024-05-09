import React, { useMemo } from 'react'
import { useTranslation } from 'react-i18next'

//styles from shared/styles/submittmentsview
const NonSubmittmentsContainer = (props) => {
  const [t, i18n] = useTranslation();
  const currentPageMembers = useMemo(() => {
    if(props.highestPage == props.currentPage){
      return props.nonSubmittments.slice((props.currentPage-1)*props.itemsPerPage)
    }
    else{
      return props.nonSubmittments.slice((props.currentPage-1)*props.itemsPerPage, props.currentPage*props.itemsPerPage)
    }
  }, [props.nonSubmittments, props.currentPage])

  return (
    <div className='SubmittmentsContainer'>{currentPageMembers?.map(i => <div className='Submittment'><p className='text-sm sm:text-base'>{i.UserName}</p><p className="text-red-400 mr-3 text-sm sm:text-base shadow-sm">{t("Text missing")}</p></div>)}</div>
  )
}

export default NonSubmittmentsContainer