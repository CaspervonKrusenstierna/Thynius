import React from 'react'
import SubmittmentPageView from './submittmentpageview/SubmittmentPageView'
import { DashboardHeader } from '../../shared/components/dashboard'

const SubmittmentView = () => {
  return (
    <>
      <DashboardHeader></DashboardHeader>
      <SubmittmentPageView></SubmittmentPageView>
    </>
  )
}

export default SubmittmentView