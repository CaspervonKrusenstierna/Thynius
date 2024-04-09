import React from 'react'
import "./TextFooter.css"
import DeleteTextButton from './DeleteTextButton/DeleteTextButton'

const TextFooter = (props) => {
  return (
    <div className='TextFooter'>
        <DeleteTextButton textId={props.textId}></DeleteTextButton>
    </div>
  )
}

export default TextFooter