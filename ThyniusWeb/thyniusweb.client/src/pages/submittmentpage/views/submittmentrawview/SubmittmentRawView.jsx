import React from 'react'
import "./SubmittmentRawView.css"

const SubmittmentRawView = (props) => {
  return (
    <div className='SubmittmentRawView'>
        <p style={{whiteSpace: "pre-wrap"}}>{props.rawText}</p>
    </div>
  )
}

export default SubmittmentRawView