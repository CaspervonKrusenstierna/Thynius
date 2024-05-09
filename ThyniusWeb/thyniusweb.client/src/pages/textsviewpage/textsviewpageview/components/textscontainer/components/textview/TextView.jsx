import React from 'react'
import "./TextView.css"
import { Link } from 'react-router-dom'
import { ArrowRight } from '../../../../../../../shared/assets'
import { useTranslation } from 'react-i18next'

const TextView = (props) => {
  const [t, i18n] = useTranslation();
  return (
    <Link className='TextView' to={"/dashboard/text/" + props.Id}>
        <div className='TextView-Title'>
          {props.Title}
        </div>
        <div className='TextView-SubmitButtonContainer'>
          <Link className="TextView-SubmitButton" to={"/dashboard/text/" + props.Id + "/submit/"}>
            <p className='TextView-SubmitButton-Text'>{t("Submit")}</p>
            <img className='TextView-SubmitButton-Img' src={ArrowRight}></img>
          </Link>
        </div>
    </Link>
  )
}

export default TextView