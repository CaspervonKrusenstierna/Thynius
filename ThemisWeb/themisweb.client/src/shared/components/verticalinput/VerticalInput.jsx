import React from 'react'
import "./VerticalInput.css"

const VerticalInput = (props) => {
  return (
    <div className='VerticalInput'>
        <p className='VerticalInput-Title'>{props.Title}</p>
        <input onChange={props.onChange} className='VerticalInput-Input'></input>
    </div>
  )
}

export default VerticalInput