import React from 'react'
import "./SubmittmentRemark.css"

const SubmittmentRemark = (props) => {
  return (
    <p className='SubmittmentRemark'>{props.remark}</p>
  )
}

export default SubmittmentRemark