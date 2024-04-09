import React from 'react'
import "./RawSubmittmentView.css"

const RawSubmittmentView = (props) => {
  return (
    <div className='RawSubmittmentView'>
        {props.rawText}
    </div>
  )
}

export default RawSubmittmentView