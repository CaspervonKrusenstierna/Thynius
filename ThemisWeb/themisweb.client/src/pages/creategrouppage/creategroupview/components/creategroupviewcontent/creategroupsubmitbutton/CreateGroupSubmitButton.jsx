import React from 'react'
import "./CreateGroupSubmitButton.css"

const CreateGroupSubmitButton = (props) => {
  return (
    <button className='CreateGroupSubmitButton' onClick={props.onClick}>
        <p className='CreateGroupSubmitButton-Text'>Skapa</p>
    </button>
  )
}

export default CreateGroupSubmitButton