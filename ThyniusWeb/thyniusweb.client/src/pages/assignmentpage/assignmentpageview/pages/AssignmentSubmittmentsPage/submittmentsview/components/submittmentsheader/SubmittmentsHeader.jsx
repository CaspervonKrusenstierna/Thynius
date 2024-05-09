import React from 'react'
import SubmittmentsFilter from './SubmittmentsFilter/SubmittmentsFilter'
import SubmittmentsSearch from "../../../../../../../../shared/components/dashboard/submittmentssearch/SubmittmentsSearch"
import { useTranslation } from 'react-i18next'
//styles from shared/styles/submittmentsview
const SubmittmentsHeader = (props) => {
  const [t, i18n] = useTranslation();
  return (
    <div className='SubmittmentsHeader'>
      <p className='SubmittmentsHeader-Title'>{t("Submittments")}</p>
      <div style={{display: "flex", flexDirection: "row", alignItems: "center", gap:"15px"}}>
        <SubmittmentsSearch setSearch={(search) => {props.setSearch(search)}}></SubmittmentsSearch>
        <SubmittmentsFilter setFilter={(filter) => {props.setFilter(filter)}}></SubmittmentsFilter>
      </div>
    </div>
  )
}

export default SubmittmentsHeader