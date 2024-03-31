import React from 'react'
import DashboardHeader from "../../shared/components/dashboardheader/DashboardHeader"
import SubmitTextPageView from './submittextpageview/SubmitTextPageView'

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