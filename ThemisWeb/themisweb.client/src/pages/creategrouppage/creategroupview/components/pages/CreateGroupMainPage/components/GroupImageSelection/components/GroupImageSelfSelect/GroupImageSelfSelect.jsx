import React from 'react'
import "./GroupImageSelfSelect.css"
import { FileRegular } from '../../../../../../../../../../shared/assets'

const GroupImageSelfSelect = () => {
  return (
    <div className='GroupImageSelfSelect-Container'>
        <div className='GroupImageSelfSelect'>
            <img className='GroupImageSelfSelect-Image-NonSelected' src={FileRegular}></img>
            <input type='file' className='GroupImageSelfSelect-Input' title=" "></input>
        </div>
        
    </div>
  )
}

export default GroupImageSelfSelect