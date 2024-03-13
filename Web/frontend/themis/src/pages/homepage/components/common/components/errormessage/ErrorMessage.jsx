import React from 'react'
import "./ErrorMessage.css"

const ErrorMessage = (props) => {
  return (
    <p className='ErrorMessage'>{props.message}</p>
  )
}

export default ErrorMessage