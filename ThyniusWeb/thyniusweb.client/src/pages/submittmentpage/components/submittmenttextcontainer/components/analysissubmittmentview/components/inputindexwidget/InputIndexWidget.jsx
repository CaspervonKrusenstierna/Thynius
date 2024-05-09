import React from 'react'
import "./InputIndexWidget.css"
import { ArrowLeft, ArrowRight, PauseImg, PlayImg } from '../../../../../../../../shared/assets'

const InputIndexWidget = (props) => {

  return (
    <div className='InputIndexWidget'>
      <button className='InputIndexWidget-Option' onClick={props.decIndex}>
        <img src={ArrowLeft} className='InputIndexWidget-OptionImg'></img>
      </button>
      <button onClick={() => {props.setIsPlaying(!props.isPlaying)}} className='InputIndexWidget-Option'>
        <img src={props.isPlaying ? PauseImg : PlayImg} className='InputIndexWidget-OptionImg'></img>
      </button>
      <button className='InputIndexWidget-Option' onClick={props.incIndex}>
        <img src={ArrowRight} className='InputIndexWidget-OptionImg'></img>
      </button>
    </div>
  )
}

export default InputIndexWidget