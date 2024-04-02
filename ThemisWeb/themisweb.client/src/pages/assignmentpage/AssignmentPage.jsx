import React from 'react'
import AssignmentPageView from './assignmentpageview/AssignmentPageView'
import { DashboardHeader } from '../../shared/components/dashboard'

const AssignmentPage = () => {
  return (
    <>
    <DashboardHeader></DashboardHeader>
    <div style={{display: "flex", justifyContent: "center", backgroundColor: "#f0f0f0", paddingBottom:"5px"}}>
        <AssignmentPageView></AssignmentPageView>
    </div>
    </>
  )
}

export default AssignmentPage