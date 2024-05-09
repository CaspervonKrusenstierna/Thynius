import React from 'react'
import "./InputsDataStat.css"

const InputsDataStat = (props) => {
  return (
    <div className='InputDataStat'>
        <div className='InputDataStat-InfoType'>{props.infoType}:</div>
        <div className='InputDataStat-Info'>{props.info}</div>
    </div>
  )
}

export default InputsDataStat