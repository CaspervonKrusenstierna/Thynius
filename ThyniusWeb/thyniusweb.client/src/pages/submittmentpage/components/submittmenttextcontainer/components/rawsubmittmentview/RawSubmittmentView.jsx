import React, { useState } from 'react'
import "./RawSubmittmentView.css"
import useSubmittmentRawText from './useSubmittmentRawText'

const RawSubmittmentView = (props) => {
  const rawText = useSubmittmentRawText(props.rawTextURL);
  return (
    <div className='RawSubmittmentView'>
        <p style={{whiteSpace: "pre-wrap"}}>{rawText}</p>
    </div>
  )
}

export default RawSubmittmentView