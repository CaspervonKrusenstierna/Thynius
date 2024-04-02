import React from 'react'
import SubmitTextPageView from './submittextpageview/SubmitTextPageView'
import { DashboardHeader } from '../../shared/components/dashboard'

const SubmitTextPage = () => {
  return (
    <>
        <DashboardHeader></DashboardHeader>
        <div className="flex justify-center">
            <SubmitTextPageView></SubmitTextPageView>
        </div>
    </>
  )
}

export default SubmitTextPage