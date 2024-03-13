import React from 'react'
import "./Input.css"

const Input = (props) => {
  if(props.hide){
    return (
      <div className='Input'>
          <h className='Input-label'>{props.label}</h>
          <div className='Input-Container'>
            <div className='Input-iconcontainer'>
              <img className="Input-icon" src={props.img}></img>
            </div>
            <input type="password" className='Input-input' onChange={props.onChange}>
            </input>
          </div>
      </div>
    )
  }
  else{
    return (
      <div className='Input'>
          <h className='Input-label'>{props.label}</h>
          <div className='Input-Container'>
            <div className='Input-iconcontainer'>
              <img className="Input-icon" src={props.img}></img>
            </div>
            <input className='Input-input' onChange={props.onChange}>
            </input>
          </div>
      </div>
    )
  }
}

export default Input