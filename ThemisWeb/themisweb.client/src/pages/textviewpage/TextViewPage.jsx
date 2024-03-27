import React from 'react'
import DashboardHeader from '../../shared/components/dashboardheader/DashboardHeader'
import TextViewPageView from './textviewpageview/TextViewPageView'

const TextViewPage = () => {
  return (
    <>
      <DashboardHeader></DashboardHeader>
      <TextViewPageView></TextViewPageView>
    </>
  )
}

export default TextViewPage