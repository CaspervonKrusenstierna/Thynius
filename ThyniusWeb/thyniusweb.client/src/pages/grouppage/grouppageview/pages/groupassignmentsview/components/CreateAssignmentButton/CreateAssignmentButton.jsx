import React from 'react'
import "./CreateAssignmentButton.css"
import { Link } from 'react-router-dom'
import { ArrowRight, PlusSolid } from '../../../../../../../shared/assets'
import { useTranslation } from 'react-i18next'

const CreateAssignmentButton = (props) => {
  const [t, i18n] = useTranslation();
  return (
    <Link to={`/dashboard/group/${props.groupId}/CreateAssignment`} className='CreateAssignmentButtonContainer'>
            <button className='CreateAssignmentButton'>
                <p className={i18n.language == "sv-SE" ? "text-[18px] font-bold" : "text-[16px] font-bold"}>{t("New Assignment")}</p>
                <img className='CreateAssignmentButton-Img' src={ArrowRight}></img>
            </button>
    </Link>
  )
}

export default CreateAssignmentButton