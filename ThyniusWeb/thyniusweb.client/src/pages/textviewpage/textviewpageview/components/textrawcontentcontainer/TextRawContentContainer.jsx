import React from 'react'
import "./TextRawContentContainer.css"

const TextRawContentContainer = (props) => {
  return (
    <div className='TextRawContentContainer'>{props.text}</div>
  )
}

export default TextRawContentContainer