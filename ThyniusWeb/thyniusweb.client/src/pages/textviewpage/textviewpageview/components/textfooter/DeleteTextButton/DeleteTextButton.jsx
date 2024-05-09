import React from 'react'
import "./DeleteTextButton.css"
import { deleteText } from '../../../../../../shared/network/text'

const DeleteTextButton = (props) => {
  function onDeleteText(){
    deleteText(props.textId);
  }
  return (
    <button className='DeleteTextButton' onClick={onDeleteText}>Ta bort text</button>
  )
}

export default DeleteTextButton