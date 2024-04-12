import React from 'react'
import "./RawSubmittmentView.css"

const RawSubmittmentView = (props) => {
  return (
    <div className='RawSubmittmentView'>
        <p style={{whiteSpace: "pre-wrap"}}>{props.rawText}</p>
    </div>
  )
}

export default RawSubmittmentView