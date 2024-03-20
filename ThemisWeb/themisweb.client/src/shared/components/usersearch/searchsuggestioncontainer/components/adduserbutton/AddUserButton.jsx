import React from 'react'
import "./AddUserButton.css"
import { PlusSolid } from '../../../../../assets'

const AddUserButton = (props) => {
  return (
    <button onClick={props.onClick} className='AddUserButton'>
        <img className="AddUserButton-Img" src={PlusSolid}></img>
    </button>
  )
}

export default AddUserButton