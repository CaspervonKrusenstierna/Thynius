import React from 'react'
import "./SubmitButton.css"

const SubmitButton = (props) => {
  return (
    <button onClick={props.onClick} className='SubmitButton'>{props.text}</button>
  )
}

export default SubmitButton