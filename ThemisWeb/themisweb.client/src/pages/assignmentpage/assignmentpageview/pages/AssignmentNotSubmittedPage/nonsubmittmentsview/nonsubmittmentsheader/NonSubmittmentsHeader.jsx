import React from 'react'
import SubmittmentsSearch from '../../../../../../../shared/components/dashboard/submittmentssearch/SubmittmentsSearch'

//styles from shared/styles/submittmentsview
const NonSubmittmentsHeader = (props) => {
  return (
    <div className='SubmittmentsHeader ml-4 md:ml-0'>
      <p className='SubmittmentsHeader-Title'>Ej inlämnade</p>
      <div className='flex flex-row md:justify-end md:pr-5' >
        <SubmittmentsSearch  setSearch={(search) => {props.setSearch(search)}}></SubmittmentsSearch>
      </div>
    </div>
  )
}

export default NonSubmittmentsHeader