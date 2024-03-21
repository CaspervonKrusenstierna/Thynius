import React from 'react'
import "./GroupMember.css"
import { TrashSolid } from '../../../../assets'

const GroupMember = (props) => {
  return (
    <div className='GroupMember'>
        <p className='GroupMember-Text'>{props.User.username}</p>
        <button className='GroupMember-DeleteButton' onClick={() => {props.onDelete(props.User)}}>
            <img className="GroupMember-DeleteImg" src={TrashSolid}></img>
        </button>
    </div>
  )
}

export default GroupMember