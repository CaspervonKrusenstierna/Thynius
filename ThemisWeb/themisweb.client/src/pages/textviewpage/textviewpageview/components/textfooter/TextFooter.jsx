import React from 'react'
import "./TextFooter.css"
import DeleteTextButton from './DeleteTextButton/DeleteTextButton'

const TextFooter = () => {
  return (
    <div className='TextFooter'>
        <DeleteTextButton></DeleteTextButton>
    </div>
  )
}

export default TextFooter