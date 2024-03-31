import React from 'react'
import "./GroupNameInput.css"

export const GroupNameInput = (props) => {
    return (
        <div className='GroupNameInput'>
            <p className='GroupNameInput-Title'>Namn</p>
            <input autocomplete="off" onChange={props.onChange} className='GroupNameInput-Input'></input>
        </div>
      )
}
