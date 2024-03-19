import React from 'react'
import "./GroupHeader.css"

const GroupHeader = (props) => {
  return (
    <div className='GroupHeader'>
      <p className='GroupHeader-Title'>{props.groupName}</p>
    </div>
  )
}

export default GroupHeader