import React, { useEffect, useState } from 'react'
import "./CheckBoxButton.css"

const CheckBoxButton = (props) => {
  const [checkboxValue, setcheckboxValue] = useState(false);
  useEffect(() => {
    props.onChange(checkboxValue);
  }, {checkboxValue})
  return (
    <button className='CheckBoxButton' onClick={() => {setcheckboxValue(!checkboxValue); }}>
        <input className="CheckBoxButton-Input" type="checkbox" checked={checkboxValue}></input>
        <div className="CheckBoxButton-Text">{props.text}</div>
    </button>
  )
}

export default CheckBoxButton