import React from 'react'
import "./AddUserButton.css"
import { PlusSolid } from '../../../../../assets'

const AddUserButton = (props) => {
  return (
    <button onClick={props.onClick} className='AddUserButton'>
      <p className='AddUserButton-Text'>Lägg Till</p>
    </button>
  )
}

export default AddUserButton