import React, { useState } from 'react'
import SubmittmentsSearch from '../../../../../../../../shared/components/dashboard/SubmittmentsSearch/SubmittmentsSearch'
import SubmittmentsFilter from './SubmittmentsFilter/SubmittmentsFilter'

//styles from shared/styles/submittmentsview
const SubmittmentsHeader = (props) => {
  return (
    <div className='SubmittmentsHeader'>
      <p className='SubmittmentsHeader-Title'>Inlämningar</p>
      <div style={{display: "flex", flexDirection: "row", alignItems: "center", gap:"15px"}}>
        <SubmittmentsSearch setSearch={(search) => {props.setSearch(search)}}></SubmittmentsSearch>
        <SubmittmentsFilter setFilter={(filter) => {props.setFilter(filter)}}></SubmittmentsFilter>
      </div>
    </div>
  )
}

export default SubmittmentsHeader