import React from 'react'
import "./AnchorButton.css"

const AnchorButton = (props) => {
  return (
    <button className='AnchorButton' onClick={props.onClick}>{props.text}</button>
  )
}

export default AnchorButton