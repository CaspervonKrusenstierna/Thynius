import React from 'react'
import SubmittmentsSearch from '../../../../../../../shared/components/dashboard/submittmentssearch/SubmittmentsSearch'
import { useTranslation } from 'react-i18next'

//styles from shared/styles/submittmentsview
const NonSubmittmentsHeader = (props) => {
  const [t, i18n] = useTranslation();
  return (
    <div className='SubmittmentsHeader ml-4 md:ml-0'>
      <p className='SubmittmentsHeader-Title'>{t("Missing texts")}</p>
      <div className='flex flex-row md:justify-end md:pr-5' >
        <SubmittmentsSearch  setSearch={(search) => {props.setSearch(search)}}></SubmittmentsSearch>
      </div>
    </div>
  )
}

export default NonSubmittmentsHeader