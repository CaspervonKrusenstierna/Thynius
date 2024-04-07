import React from 'react'
import SubmitTextPageView from './submittextpageview/SubmitTextPageView'
import { DashboardHeader } from '../../shared/components/dashboard'

const SubmitTextPage = () => {
  return (
    <>
        <DashboardHeader></DashboardHeader>
        <div className='flex flex-column justify-center w-[100vw]'>
            <SubmitTextPageView></SubmitTextPageView>
        </div>
    </>
  )
}

export default SubmitTextPage