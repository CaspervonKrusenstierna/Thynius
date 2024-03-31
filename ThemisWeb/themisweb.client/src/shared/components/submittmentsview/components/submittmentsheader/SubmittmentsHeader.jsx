import React, { useState } from 'react'
import "./SubmittmentsHeader.css"
import SubmittmentsFilter from './components/SubmittmentsFilter/SubmittmentsFilter'
import SubmittmentsSearch from './components/SubmittmentsSearch/SubmittmentsSearch'


const SubmittmentsHeader = (props) => {
  return (
    <div className='SubmittmentsHeader'>
      <p className='SubmittmentsHeader-Title'>Inlämningar</p>
      <div style={{display: "flex", flexDirection: "row", alignItems: "center", gap:"15px"}}>
        <SubmittmentsSearch setSearch={props.setSearch}></SubmittmentsSearch>
        <SubmittmentsFilter setFilter={props.setFilter}></SubmittmentsFilter>
      </div>
    </div>
  )
}

export default SubmittmentsHeader