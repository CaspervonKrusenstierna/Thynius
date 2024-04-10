import React from 'react'
import "./InterestMoment.css"

const InterestMoment = (props) => {
  return (
    <button className='InterestMoment' onClick={() => {props.onClick(props.index)}}>
      <div className='InterestMoment-ReasonText'>{props.reasonOfInterest}</div>
      <div className='InterestMoment-IndexText'>{props.index}</div>
    </button>
  )
}

export default InterestMoment