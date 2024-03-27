import React from 'react'
import "./TextView.css"
import { Link } from 'react-router-dom'

const TextView = (props) => {
  return (
    <Link className='TextView' to={"/dashboard/text/" + props.Id}>
        <div className='TextView-Preview'></div>
        <p className='TextView-Text'>{props.Title}</p>
    </Link>
  )
}

export default TextView