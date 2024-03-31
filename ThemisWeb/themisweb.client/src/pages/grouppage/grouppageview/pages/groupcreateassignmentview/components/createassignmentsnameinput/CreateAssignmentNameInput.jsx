import React from 'react'
import "./CreateAssignmentNameInput.css"

const CreateAssignmentNameInput = (props) => {
  return (
    <div className='CreateAssignmentNameInput'>
    <p className='CreateAssignmentNameInput-Title'>{props.Title}</p>
    <input autocomplete="off" onChange={props.onChange} className='CreateAssignmentNameInput-Input'></input>
</div>
  )
}

export default CreateAssignmentNameInput