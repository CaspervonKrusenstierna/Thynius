import React from 'react'
import { Link } from 'react-router-dom'
import "./InfoRow.css"
const InfoRow = (props) => {
  return (
    <Link className="InfoRow" to={props.link}>{props.text}</Link>
  )
}

export default InfoRow