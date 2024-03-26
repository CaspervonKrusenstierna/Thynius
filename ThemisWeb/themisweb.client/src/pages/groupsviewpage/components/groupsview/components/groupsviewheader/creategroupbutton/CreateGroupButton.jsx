import React from 'react'
import { Link } from 'react-router-dom'
import "./CreateGroupButton.css"
import { ArrowRight } from '../../../../../../../shared/assets'
const CreateGroupButton = () => {
  return (
    <Link to="/dashboard/creategroup" className="CreateGroupButton">
      <p className='CreateGroupButton-Text'>Ny Grupp</p>
    <img className="CreateGroupButton-Img" src={ArrowRight}></img>
    </Link>
  )
}

export default CreateGroupButton