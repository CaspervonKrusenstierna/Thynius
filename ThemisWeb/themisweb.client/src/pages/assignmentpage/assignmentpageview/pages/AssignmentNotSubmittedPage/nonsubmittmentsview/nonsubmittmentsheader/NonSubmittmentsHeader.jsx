import React from 'react'
import SubmittmentsSearch from '../../../../../../../shared/components/dashboard/SubmittmentsSearch/SubmittmentsSearch'

//styles from shared/styles/submittmentsview
const NonSubmittmentsHeader = (props) => {
  return (
    <div className='SubmittmentsHeader'>
      <p className='SubmittmentsHeader-Title'>Inlämningar</p>
      <div style={{display: "flex", flexDirection: "row", alignItems: "center", gap:"15px"}}>
        <SubmittmentsSearch setSearch={(search) => {props.setSearch(search)}}></SubmittmentsSearch>
      </div>
    </div>
  )
}

export default NonSubmittmentsHeader