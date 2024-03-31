import React from 'react'
import "./TextView.css"
import { Link } from 'react-router-dom'
import { ArrowRight } from '../../../../../../../shared/assets'

const TextView = (props) => {
  return (
    <Link className='TextView' to={"/dashboard/text/" + props.Id}>
        <div className='TextView-Preview'>
          <Link className="TextView-SubmitButton" to={"/dashboard/text/" + props.Id + "/submit/"}>
            <p className='TextView-SubmitButton-Text'>Lämna in</p>
            <img className='TextView-SubmitButton-Img' src={ArrowRight}></img>
          </Link>
        </div>
        <p className='TextView-Text'>{props.Title}</p>
    </Link>
  )
}

export default TextView