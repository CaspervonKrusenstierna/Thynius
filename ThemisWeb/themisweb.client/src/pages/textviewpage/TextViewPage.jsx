import React from 'react'
import DashboardHeader from '../../shared/components/dashboardheader/DashboardHeader'
import TextViewPageView from './textviewpageview/TextViewPageView'

const TextViewPage = () => {
  return (
    <>
      <DashboardHeader></DashboardHeader>
      <div style={{display: "flex", justifyContent: "center"}}>
      <TextViewPageView></TextViewPageView>
      </div>
    </>
  )
}

export default TextViewPage