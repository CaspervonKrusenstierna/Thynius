import React from 'react'
import "./CreateGroupSubmitButton.css"
import { PlusSolid } from '../../../../../../../shared/assets'

const CreateGroupSubmitButton = (props) => {
  return (
    <button className='CreateGroupSubmitButton' onClick={props.onClick}>
        <img src={PlusSolid} className='CreateGroupSubmitButton-Img'></img>
        <p className='CreateGroupSubmitButton-Text'>Skapa</p>
    </button>
  )
}

export default CreateGroupSubmitButton