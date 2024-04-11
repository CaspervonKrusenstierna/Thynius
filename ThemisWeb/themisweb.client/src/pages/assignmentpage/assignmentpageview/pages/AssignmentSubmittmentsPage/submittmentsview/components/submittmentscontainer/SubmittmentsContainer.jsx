import React, { useMemo } from 'react'
import Submittment from './submittment/Submittment';

//styles from shared/styles/submittmentsview
const SubmittmentsContainer = (props) => {
  const currentPageMembers = useMemo(() => {
    if(props.highestPage == props.currentPage){
      return props.submittments.slice((props.currentPage-1)*props.itemsPerPage)
    }
    else{
      return props.submittments.slice((props.currentPage-1)*props.itemsPerPage, props.currentPage*props.itemsPerPage)
    }
  }, [props.submittments, props.currentPage])

  return (
    <div className='SubmittmentsContainer'>{currentPageMembers?.map(i => <Submittment id={i.Id} username={i.UserName} warningLevel={i.WarningLevel}></Submittment>)}</div>
  )
}

export default SubmittmentsContainer