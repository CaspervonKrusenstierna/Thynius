import React from 'react'
import TextViewPageView from './textviewpageview/TextViewPageView'
import { DashboardHeader } from '../../shared/components/dashboard'

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