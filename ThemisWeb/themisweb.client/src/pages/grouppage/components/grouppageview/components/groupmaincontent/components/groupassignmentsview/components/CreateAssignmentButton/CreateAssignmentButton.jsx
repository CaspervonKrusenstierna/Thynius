import React from 'react'
import "./CreateAssignmentButton.css"
import { ArrowRight, PlusSolid } from '../../../../../../../../../../shared/assets'
import { Link } from 'react-router-dom'

const CreateAssignmentButton = (props) => {
  return (
    <Link to={`/dashboard/group/${props.groupId}/CreateAssignment`} className='CreateAssignmentButtonContainer'>
            <button className='CreateAssignmentButton'>
                <p className='CreateAssignmentButton-Text'>Ny Uppgift</p>
                <img className='CreateAssignmentButton-Img' src={ArrowRight}></img>
            </button>
    </Link>
  )
}

export default CreateAssignmentButton