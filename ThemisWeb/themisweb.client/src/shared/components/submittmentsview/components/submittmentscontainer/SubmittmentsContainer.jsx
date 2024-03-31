import React from 'react'
import "./SubmittmentsContainer.css"
import Submittment from './submittment/Submittment';

const SubmittmentsContainer = (props) => {
  return (
    <div className='SubmittmentsContainer'>{props?.submittments?.map(i => <Submittment id={i.Id} username={i.UserName} warningLevel={i.WarningLevel}></Submittment>)}</div>
  )
}

export default SubmittmentsContainer