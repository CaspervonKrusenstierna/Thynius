import React from 'react'
import "./CreateAssignmentSubmitButton.css"

const CreateAssignmentSubmitButton = (props) => {
  return (
    <button className='CreateAssignmentSubmitButton' onClick={props.onClick}>
        <p className='CreateAssignmentSubmitButton-Text'>Skapa</p>
    </button>
  )
}

export default CreateAssignmentSubmitButton