import React from 'react'
import "./GroupMember.css"
import { ExitImg } from '../../../../assets'

const GroupMember = (props) => {
  return (
    <div className='GroupMember'>
        <p>{props.User.username}</p>
        <button className='GroupMember-DeleteButton' onClick={() => {props.onDelete(props.User)}}>
            <img src={ExitImg}></img>
        </button>
    </div>
  )
}

export default GroupMember