import React from 'react'
import "./Input.css"

const Input = (props) => {
    return (
      <div className='Input'>
          <h className='Input-label'>{props.label}</h>
          <div className='Input-Container'>
            <div className='Input-iconcontainer'>
              <img className="Input-icon" src={props.img}></img>
            </div>
            <input {...props.hook} type={props.hide ? "password" : ""} className='Input-input' onChange={props.onChange}>
            </input>
          </div>
      </div>
    )
}

export default Input