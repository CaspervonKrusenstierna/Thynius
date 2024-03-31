import React from 'react'
import DashboardHeader from '../../shared/components/dashboardheader/DashboardHeader'
import AssignmentPageView from './assignmentpageview/AssignmentPageView'

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